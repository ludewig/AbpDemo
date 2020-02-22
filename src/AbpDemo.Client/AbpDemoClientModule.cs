using System;
using System.Collections.Generic;
using System.Text;
using Abp.Modules;
using AbpDemo.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Abp.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;
using AbpDemo.Configuration;

namespace AbpDemo.Client
{
    [DependsOn(typeof(AbpDemoCoreModule),
        typeof(AbpDemoApplicationModule),
        typeof(AbpDemoEntityFrameworkCoreModule))]
    public class AbpDemoClientModule:AbpModule
    {
        private readonly IConfigurationRoot _configuration;
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //base.Initialize();
        }

        public override void PreInitialize()
        {
            string connectionString = "Server=localhost; Database=AbpDemoDb; uid=root;pwd=Admin123;";
            Configuration.DefaultNameOrConnectionString = connectionString;
            Configuration.Modules.AbpEfCore().AddDbContext<AbpDemoDbContext>(options =>
            {
                options.DbContextOptions.UseMySql(connectionString);
            });
            //base.PreInitialize();
        }
    }
}
