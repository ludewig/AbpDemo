using System.Threading.Tasks;
using AbpDemo.Web.Controllers;
using Shouldly;
using Xunit;

namespace AbpDemo.Web.Tests.Controllers
{
    public class HomeController_Tests: AbpDemoWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
