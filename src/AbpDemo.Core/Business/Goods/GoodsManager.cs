using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;

namespace AbpDemo
{
    public class GoodsManager : DomainService, IGoodsManager
    {
        private readonly IRepository<Goods, string> _goodsRepository;
        private readonly IRepository<GoodsRecord, string> _recordRepository;
        public GoodsManager(IRepository<Goods, string> goodsRepository, IRepository<GoodsRecord, string> recordRepository)
        {
            _goodsRepository = goodsRepository;
            _recordRepository = recordRepository;
        }

        [UnitOfWork]
        public async Task<Goods> CheckGoods(GoodsRecord input)
        {
            Goods entity = _goodsRepository.FirstOrDefault(input.Id);
            if (entity!=null)
            {
                entity.GoodsNum = entity.GoodsNum + input.GoodsNum;
            }

            input.Id = Guid.NewGuid().ToString();
            input.OperateType = GoodsOperateType.Check;
            GoodsRecord record = await _recordRepository.InsertAsync(input);

            return await _goodsRepository.UpdateAsync(entity);
        }
    }
}
