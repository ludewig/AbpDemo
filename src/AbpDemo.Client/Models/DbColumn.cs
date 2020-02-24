using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Client
{
    public class DbColumn
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string ColumnType { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int ColumnLength { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string ColumnDescribe { get; set; }
    }
}
