using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbpDemo.EntityFrameworkCore
{
    public class AbpDemoDbContext : AbpDbContext
    {
        //在此处为实体类添加DbSet属性
        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsRecord> GoodsRecord { get; set; }

        public AbpDemoDbContext(DbContextOptions<AbpDemoDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //实体对象与数据库对象映射
            modelBuilder.ApplyConfiguration(new GoodsMap());
            modelBuilder.ApplyConfiguration(new GoodsRecordMap());
        }
    }
}
