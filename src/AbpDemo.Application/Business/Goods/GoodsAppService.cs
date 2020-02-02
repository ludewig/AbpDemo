using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.Events.Bus;
using DotNetCore.CAP;

namespace AbpDemo.Business
{
    /// <summary>
    /// 货品管理-应用服务
    /// </summary>
    public class GoodsAppService: AbpDemoAppServiceBase<Goods,DetailGoodsDto,string,CreateGoodsDto,UpdateGoodsDto,PagedGoodsDto>,IGoodsAppService
    {
        public IEventBus EventBus { get; set; }//事件总线
        private const int MinNum = 50;//货品数量下限
        private readonly IGoodsRecordManager _goodsRecordManager;//出入库记录领域服务
        private readonly IGoodsManager _goodsManager;//货品管理领域服务
        private readonly IMessageManager _messageManager;//实时消息领域服务
        private readonly ICapPublisher _capPublisher;//数据发布器
        public GoodsAppService(IRepository<Goods,string> repository,
            IGoodsRecordManager goodsRecordManager,
            IGoodsManager goodsManager,
            IMessageManager messageManager,
            ICapPublisher capPublisher) :base(repository)
        {
            _goodsRecordManager = goodsRecordManager;
            _goodsManager = goodsManager;
            _messageManager = messageManager;
            EventBus = NullEventBus.Instance;
            _capPublisher = capPublisher;
        }

        public override Task<DetailGoodsDto> Create(CreateGoodsDto input)
        {
            Goods goods = input.MapTo<Goods>();
            _capPublisher.Publish<Goods>("goods-sync", goods);//发布数据

            return base.Create(input);
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

            if (entity.GoodsNum<=MinNum)
            {
                EventBus.Trigger(new GoodsNumChangedEventData
                {
                    Id = entity.Id,
                    GoodsName = entity.GoodsName,
                    GoodsNum = entity.GoodsNum,
                    MinNum=MinNum
                }) ;

                ////注册事件
                //var goodsChangedEvent = EventBus.Register<GoodsNumChangedEventData>(data =>
                //{
                //    /*
                //     * To do
                //     **/
                //});
                ////取消注册事件
                //goodsChangedEvent.Dispose();

                string message = string.Format("货品{0}当前库存为{1}，低于最低允许库存{2}，请及时采购补充！", entity.GoodsName, entity.GoodsNum, MinNum);
                await _messageManager.BoradcastMessage(message);

            }

            DetailGoodsDto result = entity.MapTo<DetailGoodsDto>();
            return await Task.FromResult(result);
        }
    }
}
