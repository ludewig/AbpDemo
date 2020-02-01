using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo
{
    /// <summary>
    /// 出入库记录-领域服务接口
    /// </summary>
    public interface IGoodsRecordManager:IDomainService
    {
        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> InRecord(GoodsRecord input);
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> OutRecord(GoodsRecord input);
        /// <summary>
        /// 盘点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> CheckRecord(GoodsRecord input);
    }
}
