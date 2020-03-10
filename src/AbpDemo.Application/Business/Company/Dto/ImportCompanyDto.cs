using System;
using System.Collections.Generic;
using System.Text;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Spatial4n.Core.Shapes;

namespace AbpDemo
{
    /// <summary>
    /// 新增数据传输对象
    /// </summary>
    [ExcelImporter(IsLabelingError = false)]
    public class ImportCompanyDto
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        [ImporterHeader(Name = "公司名称")]
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        [ImporterHeader(Name = "法定代表人")]
        public virtual string Representative { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        [ImporterHeader(Name = "注册资本")]
        public virtual int CapitalAmount { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        [ImporterHeader(Name = "成立日期")]
        public virtual DateTime EnrollDate { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [ImporterHeader(Name = "联系电话")]
        public virtual string PhoneNum { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        [ImporterHeader(Name = "行业")]
        public virtual string Industry { get; set; }
        /// <summary>
        /// 存续状态
        /// </summary>
        [ImporterHeader(Name = "状态")]
        public virtual string Status { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        [ImporterHeader(Name="坐标")]
        public virtual string Location { get; set; }

        public double Longitude()
        {
            return Convert.ToDouble(Location.Split(',')[0]);
        }

        public double Latitude()
        {
            return Convert.ToDouble(Location.Split(',')[1]);
        }
    }
}
