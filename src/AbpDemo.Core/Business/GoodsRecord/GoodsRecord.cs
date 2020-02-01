using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo
{
    /// <summary>
    /// 货品出入库记录实体类
    /// </summary>
    public class GoodsRecord: CreationAuditedEntity<string>
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
        /// 操作类型
        /// </summary>
        public GoodsOperateType OperateType { get; set; }
        /// <summary>
        /// 货品数量
        /// </summary>
        public int GoodsNum { get; set; }
    }

    public enum GoodsOperateType
    {
        /// <summary>
        /// 入库
        /// </summary>
        In,
        /// <summary>
        /// 出库
        /// </summary>
        Out,
        /// <summary>
        /// 盘点
        /// </summary>
        Check
    }
}
