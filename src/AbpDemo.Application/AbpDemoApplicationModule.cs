using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace AbpDemo
{
    [DependsOn(
        typeof(AbpDemoCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AbpDemoApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoApplicationModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<ImportCompanyDto, Company>();
                config.CreateMap<Company, DetailCompanyDto>();
            });
        }
    }
}