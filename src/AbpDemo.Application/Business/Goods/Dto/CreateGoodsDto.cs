using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace AbpDemo.Business
{
    /// <summary>
    /// 新增货品-数据传输对象
    /// </summary>
    [AutoMapTo(typeof(Goods))]
    public class CreateGoodsDto
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

    }
}
