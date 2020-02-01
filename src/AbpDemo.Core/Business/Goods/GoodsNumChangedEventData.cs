using System;
using System.Collections.Generic;
using System.Text;
using Abp.Events.Bus;

namespace AbpDemo
{
    /// <summary>
    /// 货品库存变更事件
    /// </summary>
    public class GoodsNumChangedEventData:EventData
    {
        /// <summary>
        /// 货品标识
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 货品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 当前数量
        /// </summary>
        public int GoodsNum { get; set; }
        /// <summary>
        /// 数量下限
        /// </summary>
        public int MinNum { get; set; }
    }
}
