using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;

namespace AbpDemo
{
    public interface ISampleAppService:IApplicationService
    {
        Muyan.Search.IndexInfo Info();
        void List();

        void Detail(string id);
    }
}
