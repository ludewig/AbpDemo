using System;
using System.Threading.Tasks;
using Abp.TestBase;
using AbpDemo.EntityFrameworkCore;
using AbpDemo.Tests.TestDatas;

namespace AbpDemo.Tests
{
    public class AbpDemoTestBase : AbpIntegratedTestBase<AbpDemoTestModule>
    {
        public AbpDemoTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<AbpDemoDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<AbpDemoDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<AbpDemoDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<AbpDemoDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<AbpDemoDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<AbpDemoDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<AbpDemoDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<AbpDemoDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
