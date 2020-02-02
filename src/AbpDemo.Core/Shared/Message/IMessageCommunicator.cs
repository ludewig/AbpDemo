using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo
{
    public interface IMessageCommunicator
    {
        /// <summary>
        /// 发送消息给所有客户端
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        Task SendMessageToAll(string channel, string content);
    }
}
