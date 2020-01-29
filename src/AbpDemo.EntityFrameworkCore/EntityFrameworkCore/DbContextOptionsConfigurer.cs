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
            /* 配置数据库连接上下文 */
            //SQL Server
            //dbContextOptions.UseSqlServer(connectionString);

            //MySql
            dbContextOptions.UseMySql(connectionString);
        }
    }
}
