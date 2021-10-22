using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo
{
    public interface ICompanyAppService
    {
        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        List<DetailCompanyDto> Import(string filePath);

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <returns></returns>
        string Index();

        /// <summary>
        /// 查找附近
        /// </summary>
        /// <returns></returns>
        Dictionary<DetailCompanyDto, double> Around();
    }
}
