using System;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using AbpDemo.Configuration;
using AbpDemo.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Muyan.Search;

namespace AbpDemo.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public Startup(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            _appConfiguration = environment.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            //Configure DbContext
            services.AddAbpDbContext<AbpDemoDbContext>(options =>
            {
                DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddNewtonsoftJson(options=>options.SerializerSettings.DateFormatString="yyyy-MM-dd HH:mm:ss");

            #region swagger
            SwaggerConfiguration.ConfigSwaggerDoc(services,_appConfiguration);
            #endregion

            #region SignalR
            services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(5);//心跳间隔
            });
            #endregion

            #region CAP
            services.AddTransient<ISubscribeAppService, SubscribeAppService>();

            services.AddCap(x =>
            {
                x.UseDashboard();
                //x.UseEntityFramework<RunGoDbContext>();
                //配置数据库连接
                string connectionString = _appConfiguration["ConnectionStrings:Default"];
                x.UseMySql(connectionString);
                //配置消息队列RabbitMQ
                x.UseRabbitMQ(option =>
                {
                    option.HostName = _appConfiguration["MqSettings:MqHost"];
                    option.VirtualHost = _appConfiguration["MqSettings:MqVirtualHost"];
                    option.UserName = _appConfiguration["MqSettings:MqUserName"];
                    option.Password = _appConfiguration["MqSettings:MqPassword"];
                });
            });

            #endregion

            #region Search

            services.AddSearchManager(new SearchManagerConfig()
            {
                DefaultPath = _appConfiguration["Search:DefaultPath"],
                FacetPath = _appConfiguration["Search:FacetPath"]
            });

            #endregion


            //Configure Abp and Dependency Injection
            return services.AddAbp<AbpDemoWebModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); //Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            #region swagger
            SwaggerConfiguration.ConfigeEndpoint(app,_appConfiguration);
            #endregion

            #region SignalR
            app.UseSignalR(config =>
            {
                config.MapHub<MessageHub>("/messagebus");
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
