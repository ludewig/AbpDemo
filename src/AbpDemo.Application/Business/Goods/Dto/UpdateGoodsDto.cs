using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace AbpDemo.Business
{
    /// <summary>
    /// 修改货品-数据传输对象
    /// </summary>
    [AutoMapTo(typeof(Goods))]
    public class UpdateGoodsDto:EntityDto<string>
    {
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
