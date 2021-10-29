using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpDemo.Web.Controllers
{
    public class SampleController : AbpDemoControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Statistic()
        {
            return View();
        }
    }
}
