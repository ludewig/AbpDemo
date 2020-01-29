using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace AbpDemo.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpDemoCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class AbpDemoEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoEntityFrameworkCoreModule).GetAssembly());
        }
    }
}