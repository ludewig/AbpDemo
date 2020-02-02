using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;

namespace AbpDemo
{
    public interface ISubscribeAppService:ICapSubscribe
    {
        void SubscribeGoods(Goods goods);
    }
}
