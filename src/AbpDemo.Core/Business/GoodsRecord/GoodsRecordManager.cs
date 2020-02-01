using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Domain.Repositories;


namespace AbpDemo
{
    /// <summary>
    /// 出入库记录-领域服务
    /// </summary>
    public class GoodsRecordManager : DomainService, IGoodsRecordManager
    {
        private readonly IRepository<GoodsRecord, string> _recordRepository;//仓储
        public GoodsRecordManager(IRepository<GoodsRecord, string> recordRepository)
        {
            _recordRepository = recordRepository;
        }
        /// <summary>
        /// 盘点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> CheckRecord(GoodsRecord input)
        {
            input.Id = Guid.NewGuid().ToString();
            return await _recordRepository.InsertAndGetIdAsync(input);
        }
        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> InRecord(GoodsRecord input)
        {
            input.Id = Guid.NewGuid().ToString();
            return await _recordRepository.InsertAndGetIdAsync(input);
        }
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> OutRecord(GoodsRecord input)
        {
            input.Id = Guid.NewGuid().ToString();
            return await _recordRepository.InsertAndGetIdAsync(input);
        }
    }
}
