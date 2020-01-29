using Abp.AspNetCore.Mvc.Views;

namespace AbpDemo.Web.Views
{
    public abstract class AbpDemoRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected AbpDemoRazorPage()
        {
            LocalizationSourceName = AbpDemoConsts.LocalizationSourceName;
        }
    }
}
