using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Muyan.Search;

namespace AbpDemo
{
    public class SampleAppService: ApplicationService,ISampleAppService
    {
        private readonly ISampleManager _sampleManager;
        public SampleAppService(ISampleManager sampleManager)
        {
            _sampleManager = sampleManager;
        }

        public IndexInfo Info()
        {
            return _sampleManager.GetIndexInfo();
        }

        public void List()
        {
            
        }

        public void Detail(string id)
        {
            
        }
    }
}
