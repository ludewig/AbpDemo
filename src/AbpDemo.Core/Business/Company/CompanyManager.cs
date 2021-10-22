
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Muyan.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo
{
    /// <summary>
    /// 领域服务类
    /// </summary>
    public class CompanyManager : DomainService, ICompanyManager
    {
        private readonly IRepository<Company, string> _repository;
        private readonly ISearchManager _searchManager;

        public CompanyManager(IRepository<Company, string> repository,ISearchManager searchManager)
        {
            _repository = repository;
            _searchManager = searchManager;
        }

        public List<Company> Import(List<Company> entities)
        {
            List<Company> results = new List<Company>();
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid().ToString();
                //_searchManager.CreateIndexByEntity(entity, true);
                var result = _repository.Insert(entity);
                results.Add(result);
            }
            return results;
        }

        public int CreateIndex()
        {
            int n = 0;
            List<Company> entities = _repository.GetAllList();
            foreach (var entity in entities)
            {
                _searchManager.CreateIndexByEntity(entity,true);
            }

            n = _searchManager.CountIndex();
            return n;
        }

        public Dictionary<Company, double> SearchAround(double radius)
        {
            Dictionary<Company, double> dic = new Dictionary<Company, double>();
            GeoSearchOption option = new GeoSearchOption();
            option.Raidus = radius;
            var docs = _searchManager.GeoSearch(option);
            foreach (var item in docs.Items)
            {
                string id = item.Key.Get("id");
                if (string.IsNullOrEmpty(id))
                {
                    Company entity = _repository.Get(id);
                    dic.Add(entity,item.Value);
                }
                else
                {
                    continue;
                }
            }
            return dic;
        }
    }
}
