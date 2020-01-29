using Microsoft.AspNetCore.Mvc;

namespace AbpDemo.Web.Controllers
{
    public class HomeController : AbpDemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}