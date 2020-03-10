
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Spatial4n.Core.Shapes;
using Spatial4n.Core.Shapes.Impl;

namespace AbpDemo
{
    /// <summary>
    /// 实体映射
    /// </summary>
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("info_company");
            builder.HasKey(m => m.Id);//主键
            builder.Property(m => m.Id).HasColumnName("RowGuid");//列名
            builder.Property(m => m.CompanyName).HasMaxLength(100);
            builder.Property(m => m.Representative).HasMaxLength(50);
            builder.Property(m => m.CapitalAmount).HasMaxLength(10);
            builder.Property(m => m.EnrollDate).HasMaxLength(20);
            builder.Property(m => m.PhoneNum).HasMaxLength(15);
            builder.Property(m => m.Industry).HasMaxLength(50);
            builder.Property(m => m.Status).HasMaxLength(50);
            builder.Property(m => m.Longitude).HasMaxLength(20).HasDefaultValue(118.778074408);
            builder.Property(m => m.Latitude).HasMaxLength(20).HasDefaultValue(32.0572355018);
        }
    }
}
