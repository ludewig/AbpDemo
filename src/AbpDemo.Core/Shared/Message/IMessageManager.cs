using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo
{
    public interface IMessageManager : IDomainService
    {
        Task BoradcastMessage(object obj);
    }
}
