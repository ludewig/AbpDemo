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
    }
}
