using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Business
{
    public class InOutGoodsDto:EntityDto<string>
    {
        /// <summary>
        /// 货品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 货品数量
        /// </summary>
        public int GoodsNum { get; set; }
    }
}
