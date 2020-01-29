using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace AbpDemo
{
    /// <summary>
    /// 货品实体类
    /// </summary>
    public class Goods:FullAuditedEntity<string>
    {
        /// <summary>
        /// 货品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 货品类型
        /// </summary>
        public string GoodsType { get; set; }
        /// <summary>
        /// 存放位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 货品数量
        /// </summary>
        public int GoodsNum { get; set; }
    }
}
