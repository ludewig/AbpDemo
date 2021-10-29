using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Services;

namespace AbpDemo
{
    public interface ISampleManager:IDomainService
    {
        /// <summary>
        /// 获取索引基本信息
        /// </summary>
        /// <returns></returns>
        Muyan.Search.IndexInfo GetIndexInfo();
    }
}
