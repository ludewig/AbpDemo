using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Client
{
    public class DbTable
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string TableDescribe { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        public List<DbColumn> Columns { get; set; }
    }
}
