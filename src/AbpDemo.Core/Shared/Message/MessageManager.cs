using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo
{
    /// <summary>
    /// 实时消息-领域服务
    /// </summary>
    public class MessageManager : DomainService, IMessageManager
    {
        private readonly IMessageCommunicator _messageCommunicator;
        public MessageManager(IMessageCommunicator messageCommunicator)
        {
            _messageCommunicator = messageCommunicator;
        }

        public async Task BoradcastMessage(object obj)
        {
            string content = obj.ToString();
            await _messageCommunicator.SendMessageToAll("broadcast", content);
        }
    }
}
