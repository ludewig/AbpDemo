using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.SignalR;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Auditing;
using Abp.Dependency;
using Abp.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace AbpDemo.Web
{
    /// <summary>
    /// 实时消息集线器类
    /// </summary>
    public class MessageHub:OnlineClientHubBase
    {
        private readonly IMessageManager _messageManager;
        public MessageHub(IMessageManager messageManager,
            IOnlineClientManager onlineClientManager,
            IClientInfoProvider clientProvider):base(onlineClientManager,clientProvider)
        {
            _messageManager = messageManager;
        }

        //消息广播
        public async Task Broadcast(string message)
        {
            //await Clients.All.SendAsync(message);
            await _messageManager.BoradcastMessage(message);
        }
    }
}
