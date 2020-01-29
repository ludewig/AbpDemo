using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Abp.Runtime.Caching;
using System.Linq.Expressions;


namespace AbpDemo
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class AbpDemoAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TCreateInput, TUpdateInput> : ApplicationService,
        IAbpDemoAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdateInput : class, IEntityDto<TPrimaryKey>
        where TEntityDto : class, IEntityDto<TPrimaryKey>
    {
        protected readonly IRepository<TEntity, TPrimaryKey> Repository;

        protected AbpDemoAppServiceBase()
        {
            LocalizationSourceName = AbpDemoConsts.LocalizationSourceName;
        }

        #region 方法

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual Task<TEntityDto> Create(TCreateInput input)
        {
            TEntity entity = input.MapTo<TEntity>();
            string id = Guid.NewGuid().ToString();
            entity.Id = ChangeTo(id);
            TEntity result = Repository.Insert(entity);
            return Task.FromResult<TEntityDto>(result.MapTo<TEntityDto>());
        }

        private static TPrimaryKey ChangeTo(string str)
        {
            TPrimaryKey result = default(TPrimaryKey);
            try
            {
                result = (TPrimaryKey)Convert.ChangeType(str, typeof(TPrimaryKey));
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual Task<TEntityDto> Update(TUpdateInput input)
        {
            TEntity entity = input.MapTo<TEntity>();
            TEntity result = Repository.Update(entity);
            return Task.FromResult<TEntityDto>(result.MapTo<TEntityDto>());
        }
        #endregion

        #region 详情
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<TEntityDto> Detail(TPrimaryKey id)
        {
            TEntity result = Repository.Get(id);
            return Task.FromResult<TEntityDto>(result.MapTo<TEntityDto>());
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual Task<bool> Delete(IEnumerable<TPrimaryKey> ids)
        {
            try
            {
                foreach (TPrimaryKey id in ids)
                {
                    Repository.Delete(id);
                }
                return Task.FromResult<bool>(true);
            }
            catch (System.Exception ex)
            {
                return Task.FromException<bool>(ex);
            }
        }
        #endregion

        #endregion

    }
}