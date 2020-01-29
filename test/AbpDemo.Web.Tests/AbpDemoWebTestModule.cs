using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpDemo.Web.Startup;
namespace AbpDemo.Web.Tests
{
    [DependsOn(
        typeof(AbpDemoWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class AbpDemoWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoWebTestModule).GetAssembly());
        }
    }
}