using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace AbpDemo
{
    public interface IGoodsManager:IDomainService
    {
        Task<Goods> CheckGoods(GoodsRecord input);
    }
}
