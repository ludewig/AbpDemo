using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;

namespace AbpDemo
{
    /// <summary>
    /// 数据订阅服务
    /// </summary>
    public class SubscribeAppService : ISubscribeAppService
    {
        /// <summary>
        /// 货品数据同步
        /// </summary>
        /// <param name="goods"></param>
        [CapSubscribe("goods-sync")]
        public void SubscribeGoods(Goods goods)
        {
            //...
        }
    }
}
