using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Services;
using Muyan.Search;

namespace AbpDemo
{
    public class SampleManager:DomainService,ISampleManager
    {
        private readonly ISearchManager _searchManager;
        public SampleManager(ISearchManager searchManager)
        {
            _searchManager = searchManager;
        }

        public IndexInfo GetIndexInfo()
        {
            return _searchManager.Info();
        }
    }
}
