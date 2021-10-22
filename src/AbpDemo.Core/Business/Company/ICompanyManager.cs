
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo
{
    /// <summary>
    /// 领域服务接口
    /// </summary>
    public interface ICompanyManager : IDomainService
    {
        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        List<Company> Import(List<Company> entities);

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <returns></returns>
        int CreateIndex();

        /// <summary>
        /// 查询半径范围内的对象
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        Dictionary<Company, double> SearchAround(double radius);
    }
}
