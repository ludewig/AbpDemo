using Microsoft.EntityFrameworkCore;

namespace AbpDemo.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<AbpDemoDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for AbpDemoDbContext */
            //dbContextOptions.UseSqlServer(connectionString);
            dbContextOptions.UseMySql(connectionString);
        }
    }
}
