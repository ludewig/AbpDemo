using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbpDemo
{
    public class GoodsMap : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> builder)
        {
            builder.ToTable("INFO_Goods");//表名
            builder.HasKey(m => m.Id);//主键
            builder.Property(m => m.Id).HasColumnName("RowGuid");//列名
            builder.Property(m => m.GoodsName)
                //.HasColumnType("varchar")//数据类型
                .HasMaxLength(100)//数据长度
                .HasDefaultValue("默认值")//默认值
                .IsRequired(true);//true不可为空/false可为空
            builder.Property(m => m.Location).HasMaxLength(100);
            builder.Property(m => m.GoodsNum).HasMaxLength(10).HasDefaultValue(0);
        }
    }
}
