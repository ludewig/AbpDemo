using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo
{
    public class GoodsRecordMap : IEntityTypeConfiguration<GoodsRecord>
    {
        public void Configure(EntityTypeBuilder<GoodsRecord> builder)
        {
            builder.ToTable("INFO_GoodsRecord");//表名
            builder.HasKey(m => m.Id);//主键
            builder.Property(m => m.Id).HasColumnName("RowGuid");//列名
            builder.Property(m => m.GoodsName)
                //.HasColumnType("varchar")//数据类型
                .HasMaxLength(100)//数据长度
                .HasDefaultValue("默认值")//默认值
                .IsRequired(true);//true不可为空/false可为空
            builder.Property(m => m.GoodsType).HasMaxLength(50);
            builder.Property(m => m.OperateType).HasMaxLength(10);
            builder.Property(m => m.Location).HasMaxLength(50);
            builder.Property(m => m.GoodsNum).HasMaxLength(10).HasDefaultValue(0);
        }
    }
}
