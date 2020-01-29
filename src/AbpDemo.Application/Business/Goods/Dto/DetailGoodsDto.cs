using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace AbpDemo.Business
{
    /// <summary>
    /// 货品详情-数据传输对象
    /// </summary>
    [AutoMapFrom(typeof(Goods))]
    public class DetailGoodsDto:EntityDto<string>
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
