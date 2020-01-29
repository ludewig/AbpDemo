using Abp.AspNetCore.Mvc.Controllers;

namespace AbpDemo.Web.Controllers
{
    public abstract class AbpDemoControllerBase: AbpController
    {
        protected AbpDemoControllerBase()
        {
            LocalizationSourceName = AbpDemoConsts.LocalizationSourceName;
        }
    }
}