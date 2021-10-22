
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace AbpDemo
{
    /// <summary>
    /// 应用服务类
    /// </summary>
    public class CompanyAppService : ApplicationService, ICompanyAppService
    {
        private readonly IRepository<Company, string> _repository;
        private readonly ICompanyManager _manager;
        private IImporter _importer = new ExcelImporter();

        public CompanyAppService(IRepository<Company, string> repository,ICompanyManager manager)
        {
            _repository = repository;
            _manager = manager;
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpGet]
        public List<DetailCompanyDto> Import(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new UserFriendlyException("文件路径不能为空！");
            }

            if (!System.IO.File.Exists(filePath))
            {
                throw new UserFriendlyException("文件不存在！");
            }

            var dtos = _importer.Import<ImportCompanyDto>(filePath).Result.Data.ToList();

            //return new List<DetailCompanyDto>();
            List<Company> entities = dtos.MapTo<List<Company>>();
            entities = _manager.Import(entities);
            return entities.MapTo<List<DetailCompanyDto>>();
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Index()
        {
            string result = "";
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int num = _manager.CreateIndex();
            watch.Stop();
            result = string.Format("num:{0},elapse:{1}", num, watch.ElapsedMilliseconds);
            return result;
        }

        /// <summary>
        /// 查找附近
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Dictionary<DetailCompanyDto, double> Around()
        {
            Dictionary<DetailCompanyDto, double> result = new Dictionary<DetailCompanyDto, double>();
            var entities = _manager.SearchAround(1);
            result = entities.MapTo<Dictionary<DetailCompanyDto, double>>();
            return result;
        }
    }
}
