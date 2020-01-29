using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbpDemo.EntityFrameworkCore
{
    public class AbpDemoDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public AbpDemoDbContext(DbContextOptions<AbpDemoDbContext> options) 
            : base(options)
        {

        }
    }
}
