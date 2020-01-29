using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo
{
    public interface IAbpDemoAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TCreateInput, TUpdateInput> : IApplicationService
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdateInput : class, IEntityDto<TPrimaryKey>
        where TEntityDto : class, IEntityDto<TPrimaryKey>
    {

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input">新增数据传输对象</param>
        /// <returns></returns>
        Task<TEntityDto> Create(TCreateInput input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input">修改数据传输对象</param>
        /// <returns></returns>
        Task<TEntityDto> Update(TUpdateInput input);

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns></returns>
        Task<TEntityDto> Detail(TPrimaryKey id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        /// <returns></returns>
        Task<bool> Delete(IEnumerable<TPrimaryKey> ids);

    }
}
