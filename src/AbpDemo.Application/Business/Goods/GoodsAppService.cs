using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.AutoMapper;

namespace AbpDemo.Business
{
    /// <summary>
    /// 货品管理-应用服务
    /// </summary>
    public class GoodsAppService: AbpDemoAppServiceBase<Goods,DetailGoodsDto,string,CreateGoodsDto,UpdateGoodsDto,PagedGoodsDto>,IGoodsAppService
    {
        public GoodsAppService(IRepository<Goods,string> repository):base(repository)
        {

        }


        ///// <summary>
        ///// 入库
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<DetailGoodsDto> In(InOutGoodsDto input)
        //{
        //    DetailGoodsDto result = new DetailGoodsDto();
        //    return await Task.FromResult(result);
        //}
        ///// <summary>
        ///// 出库
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<DetailGoodsDto> Out(InOutGoodsDto input)
        //{
        //    DetailGoodsDto result = new DetailGoodsDto();
        //    return await Task.FromResult(result);
        //}
    }
}
