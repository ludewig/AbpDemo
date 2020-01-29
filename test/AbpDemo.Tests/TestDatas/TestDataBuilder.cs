using AbpDemo.EntityFrameworkCore;

namespace AbpDemo.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly AbpDemoDbContext _context;

        public TestDataBuilder(AbpDemoDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}