using Microsoft.EntityFrameworkCore;
using WebApplication1.Contexts;

namespace ApiTest.Services
{
    public class DbContextFixture : IDisposable
    {
        public ProductDbContext ctx { get; private set; }
        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            ctx = new ProductDbContext(options);
        }
        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}
