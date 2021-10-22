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
using Abp.UI;
using Microsoft.AspNetCore.Mvc;


namespace AbpDemo
{
    /// <summary>
    /// 应用服务基类
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TEntityDto">详情数据传输对象</typeparam>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    /// <typeparam name="TCreateInput">新增数据传输对象</typeparam>
    /// <typeparam name="TUpdateInput">修改数据传输对象</typeparam>
    /// <typeparam name="TPagedInput">分页数据传输对象</typeparam>
    public abstract class AbpDemoAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TCreateInput, TUpdateInput, TPagedInput> : ApplicationService,
        IAbpDemoAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TCreateInput, TUpdateInput, TPagedInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TUpdateInput : class, IEntityDto<TPrimaryKey>
        where TEntityDto : class, IEntityDto<TPrimaryKey>
        where TPagedInput:PagedBaseDto
    {
        //仓储
        protected readonly IRepository<TEntity, TPrimaryKey> Repository;

        protected AbpDemoAppServiceBase()
        {
            LocalizationSourceName = AbpDemoConsts.LocalizationSourceName;
        }

        protected AbpDemoAppServiceBase(IRepository<TEntity, TPrimaryKey> repository)
        {
            LocalizationSourceName = AbpDemoConsts.LocalizationSourceName;
            Repository = repository;
        }

        #region 方法

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
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
        [HttpPost]
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
        [HttpGet]
        public virtual Task<TEntityDto> Detail(TPrimaryKey id)
        {
            try
            {
                TEntity result = Repository.Get(id);
                return Task.FromResult<TEntityDto>(result.MapTo<TEntityDto>());
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("你所查找的数据不存在！",ex);
            }

        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual Task<int> Delete(IEnumerable<TPrimaryKey> ids)
        {
            int num = 0;
            try
            {
                foreach (TPrimaryKey id in ids)
                {
                    Repository.Delete(id);
                    num++;
                }
                return Task.FromResult<int>(num);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<PagedResultDto<TEntityDto>> Page(TPagedInput input)
        {
            if (input == null)
            {
                return new PagedResultDto<TEntityDto>();
            }
            PagedResultDto<TEntityDto> result = new PagedResultDto<TEntityDto>();
            IQueryable<TEntity> query = Repository.GetAll();
            var sourceExpression = query.Expression;

            //where表达式
            if (input.Filters != null)
            {
                foreach (var filter in input.Filters)
                {
                    if (string.IsNullOrWhiteSpace(filter.FilterName) || string.IsNullOrWhiteSpace(filter.FilterValue))
                    {
                        continue;
                    }
                    var whereLambdaExtenstion = GetLambdaExtention(filter);
                    sourceExpression = Expression.Call(typeof(Queryable), "Where", new Type[1] { typeof(TEntity) }, sourceExpression, Expression.Quote(whereLambdaExtenstion.GetLambda()));
                }
            }

            if (input.Sorts != null)
            {
                ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
                string methodAsc = "OrderBy";
                string methodDesc = "OrderByDescending";
                foreach (var sort in input.Sorts)
                {
                    if (string.IsNullOrWhiteSpace(sort.SortName))
                    {
                        continue;
                    }
                    MemberExpression body = Expression.PropertyOrField(parameter, sort.SortName);
                    sourceExpression = Expression.Call(typeof(Queryable), sort.SortType == SortType.Asc ? methodAsc : methodDesc, new Type[] { typeof(TEntity), body.Type }, sourceExpression, Expression.Quote(Expression.Lambda(body, parameter)));
                    methodAsc = "ThenBy";
                    methodDesc = "ThenByDescending";
                }
            }

            query = query.Provider.Execute<IEnumerable<TEntity>>(sourceExpression).AsQueryable();

            result.TotalCount = query.Count();
            if (input.PageSize != -1)
            {
                result.Items = query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).MapTo<IEnumerable<TEntityDto>>().ToList();
            }
            else
            {
                result.Items = query.MapTo<IEnumerable<TEntityDto>>().ToList();
            }

            return await Task.FromResult(result);
        }

        private LambdaExtention<TEntity> GetLambdaExtention(DataFilter filter)
        {
            var whereLambdaExtenstion = new LambdaExtention<TEntity>();

            switch (filter.FilterType)
            {
                case FilterType.Int:
                    whereLambdaExtenstion.GetExpression(filter.FilterName, int.Parse(filter.FilterValue), filter.ExpressionType);
                    break;
                case FilterType.Long:
                    whereLambdaExtenstion.GetExpression(filter.FilterName, long.Parse(filter.FilterValue), filter.ExpressionType);
                    break;
                case FilterType.Boolean:
                    whereLambdaExtenstion.GetExpression(filter.FilterName, filter.FilterValue.ToUpper() == "TRUE" ? true : false, filter.ExpressionType);
                    break;
                default:
                    whereLambdaExtenstion.GetExpression(filter.FilterName, filter.FilterValue, filter.ExpressionType);
                    break;
            }
            return whereLambdaExtenstion;

        }
        #endregion


        #endregion

    }
}