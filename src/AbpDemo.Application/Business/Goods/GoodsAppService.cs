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
        private readonly IGoodsRecordManager _goodsRecordManager;
        private readonly IGoodsManager _goodsManager;
        public GoodsAppService(IRepository<Goods,string> repository,IGoodsRecordManager goodsRecordManager,IGoodsManager goodsManager):base(repository)
        {
            _goodsRecordManager = goodsRecordManager;
            _goodsManager = goodsManager;
        }

        /// <summary>
        /// 盘点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<DetailGoodsDto> Check(InOutGoodsDto input)
        {
            GoodsRecord record = input.MapTo<GoodsRecord>();
            Goods result = await _goodsManager.CheckGoods(record);
            return result.MapTo<DetailGoodsDto>();
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<DetailGoodsDto> In(InOutGoodsDto input)
        {
            Goods entity = Repository.FirstOrDefault(input.Id);
            if (entity != null)
            {
                entity.GoodsNum = entity.GoodsNum + input.GoodsNum;
            }
            GoodsRecord record = input.MapTo<GoodsRecord>();
            record.OperateType = GoodsOperateType.In;
            string recordId = await _goodsRecordManager.InRecord(record);

            entity =await Repository.UpdateAsync(entity);

            DetailGoodsDto result = entity.MapTo<DetailGoodsDto>();
            return await Task.FromResult(result);
        }
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<DetailGoodsDto> Out(InOutGoodsDto input)
        {
            Goods entity = Repository.FirstOrDefault(input.Id);
            if (entity != null)
            {
                entity.GoodsNum = entity.GoodsNum - input.GoodsNum;
            }
            GoodsRecord record = input.MapTo<GoodsRecord>();
            record.OperateType = GoodsOperateType.Out;
            string recordId = await _goodsRecordManager.OutRecord(record);

            entity = await Repository.UpdateAsync(entity);

            DetailGoodsDto result = entity.MapTo<DetailGoodsDto>();
            return await Task.FromResult(result);
        }
    }
}
