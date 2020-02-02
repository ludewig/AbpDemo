using Abp.Dependency;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpDemo.Web
{
    public class MessageCommunicator : IMessageCommunicator, ITransientDependency
    {
        private readonly IHubContext<MessageHub> _hubContext;
        public MessageCommunicator(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// 发送消息给所有客户端
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public async Task SendMessageToAll(string channel, string content)
        {
            await _hubContext.Clients.All.SendAsync(channel, content);
        }
    }
}
