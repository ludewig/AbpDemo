using System;
using System.IO;
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
using Microsoft.OpenApi.Models;
using Abp.Extensions;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using AbpDemo.Web.Shared.Swagger;
using Swashbuckle.AspNetCore.Swagger;


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
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddNewtonsoftJson(options=>options.SerializerSettings.DateFormatString="yyyy-MM-dd HH:mm:ss");

            #region swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "AbpDemo",
                        Version = "v1.0",
                        Description = "2019年示例",
                        TermsOfService = new Uri("https://github.com/ludewig"),
                        Contact = new OpenApiContact { Name = "ludewig", Email = "panshuairg@hotmail.com", Url = new Uri("https://github.com/ludewig") }
                    });
                options.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Title = "AbpDemo",
                        Version = "v2.0",
                        Description = "2020年示例",
                        TermsOfService = new Uri("https://github.com/ludewig"),
                        Contact = new OpenApiContact { Name = "ludewig", Email = "panshuairg@hotmail.com", Url = new Uri("https://github.com/ludewig") }
                    });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomDefaultSchemaIdSelector();

                var filePath = Path.Combine(AppContext.BaseDirectory, "AbpDemo.Application.xml");
                options.IncludeXmlComments(filePath);
            });
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
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                string[] endpoints = _appConfiguration["App:SwaggerEndPoint"].Split('&');
                string[] names = _appConfiguration["App:SwaggerName"].Split('&');
                for (int i = 0; i < endpoints.Length; i++)
                {
                    options.SwaggerEndpoint(endpoints[i], names[i]);
                }
                //options.IndexStream = () => Assembly.GetExecutingAssembly().GetManifestResourceStream("AbpDemo.Web.wwwroot.swagger.ui.index.html");
                //options.InjectJavascript("AbpDemo.Web.wwwroot.swagger.ui.index.html");
                //options.InjectBaseUrl(_appConfiguration["App:WebSiteRootAddress"]);
            });
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
