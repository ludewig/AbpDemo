using System;
using System.IO;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using AbpDemo.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AbpDemo.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;

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
                    new OpenApiInfo { 
                        Title = "AbpDemo", 
                        Version = "v1.0",
                        Description="",
                        TermsOfService=new Uri("https://github.com/ludewig"),
                        Contact = new OpenApiContact { Name="ludewig",Email="panshuairg@hotmail.com",Url=new Uri("https://github.com/ludewig") }
                    });
                options.DocInclusionPredicate((docName, description) => true);

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API v1");
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
