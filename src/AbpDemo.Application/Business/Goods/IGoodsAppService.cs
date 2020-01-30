using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo.Business
{
    /// <summary>
    /// 货品管理-应用服务接口
    /// </summary>
    public interface IGoodsAppService: IAbpDemoAppServiceBase<Goods,DetailGoodsDto,string,CreateGoodsDto,UpdateGoodsDto, PagedGoodsDto>,IApplicationService
    {
        ///// <summary>
        ///// 入库
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<DetailGoodsDto> In(InOutGoodsDto input);
        ///// <summary>
        ///// 出库
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<DetailGoodsDto> Out(InOutGoodsDto input);
    }
}
