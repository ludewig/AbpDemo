using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo
{
    public class GoodsChangedManager : IEventHandler<GoodsNumChangedEventData>, ITransientDependency
    {
        public void HandleEvent(GoodsNumChangedEventData eventData)
        {
            string message = string.Format("货品{0}当前库存为{1}，低于最低允许库存{2}，请及时采购补充！", eventData.GoodsName, eventData.GoodsNum, eventData.MinNum);

            /*
             * To do
             * 后续处理
             * */
        }
    }
}
