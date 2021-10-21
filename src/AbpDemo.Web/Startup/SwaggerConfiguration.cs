using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AbpDemo.Web.Shared.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AbpDemo.Web.Startup
{
    public class SwaggerConfiguration
    {
        /// <summary>
        /// 配置Swagger生成的文档信息，如
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigSwaggerDoc(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(configuration["Swagger:SwaggerName"],
                    new OpenApiInfo
                    {
                        Title = configuration["Swagger:SwaggerTitle"],
                        Version = "v1.0",
                        Description = "API接口文档"
                    });

                options.DocInclusionPredicate((docName, description) => true);
                options.CustomDefaultSchemaIdSelector();

                var filePath = Path.Combine(AppContext.BaseDirectory, configuration["Swagger:SwaggerXmlName"]);
                options.IncludeXmlComments(filePath, true);
            });
        }

        /// <summary>
        /// 配置Swagger的Endpoint
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void ConfigeEndpoint(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "";
                options.DocumentTitle = configuration["Swagger:SwaggerTitle"];
                options.SwaggerEndpoint(configuration["Swagger:SwaggerEndPoint"], configuration["Swagger:SwaggerName"]);

            });
        }
    }
}
