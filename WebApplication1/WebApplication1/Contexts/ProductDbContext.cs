using Microsoft.EntityFrameworkCore;
using WebApplication1.Domains;

namespace WebApplication1.Contexts
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {

        }
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }


    }
}

