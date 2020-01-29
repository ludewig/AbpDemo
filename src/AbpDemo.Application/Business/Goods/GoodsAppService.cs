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
    public class GoodsAppService: IGoodsAppService
    {
        /// <summary>
        /// 仓储
        /// </summary>
        private readonly IRepository<Goods, string> _repository;
        public GoodsAppService(IRepository<Goods,string> repository)
        {
            _repository = repository;
        }


        #region 方法

        #region 新增
        public Task<DetailGoodsDto> Create(CreateGoodsDto input)
        {
            Goods entity = input.MapTo<Goods>();
            entity.Id = Guid.NewGuid().ToString();
            Goods result = _repository.Insert(entity);
            DetailGoodsDto output = result.MapTo<DetailGoodsDto>();
            return Task.FromResult(output);
        }
        #endregion

        #region 删除
        public Task<int> Delete(IEnumerable<string> ids)
        {
            int num = 0;
            foreach (string id in ids)
            {
                _repository.Delete(id);
                num++;
            }
            return Task.FromResult(num);
        }
        #endregion

        #region 详情
        public Task<DetailGoodsDto> Detail(string id)
        {
            Goods result = _repository.Get(id);
            DetailGoodsDto output = result.MapTo<DetailGoodsDto>();
            return Task.FromResult(output);
        }
        #endregion

        #region 修改
        public Task<DetailGoodsDto> Update(UpdateGoodsDto input)
        {
            Goods entity = input.MapTo<Goods>();
            Goods result = _repository.Update(entity);
            DetailGoodsDto output = result.MapTo<DetailGoodsDto>();
            return Task.FromResult(output);
        } 
        #endregion

        #endregion

    }
}
