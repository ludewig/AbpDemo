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
    public interface IGoodsAppService: IApplicationService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DetailGoodsDto> Create(CreateGoodsDto input);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DetailGoodsDto> Update(UpdateGoodsDto input);
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DetailGoodsDto> Detail(string id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Delete(IEnumerable<string> ids);
    }
}
