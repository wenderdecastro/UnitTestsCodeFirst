using Microsoft.EntityFrameworkCore;
using WebApplication1.Contexts;
using WebApplication1.Domains;
using WebApplication1.Interfaces;

namespace WebApplication1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext ctx;
        public ProductRepository(ProductDbContext ctx)
        {
            this.ctx = ctx;
        }
        public Product Create(Product products)
        {
            var product = new Product
            {
                Id = products.Id,
                Name = products.Name,
                Price = products.Price,
            };
            ctx.Products.Add(product);
            ctx.SaveChanges();

            return product;
        }

        public void DeleteById(Guid id)
        {
            var product = ctx.Products.Find(id);
            ctx.Products.Remove(product);
            ctx.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return ctx.Products.OrderBy(x => x.Name).ToList();
        }

        public Product GetById(Guid id)
        {
            return ctx.Products.Find(id);
        }

        public void UpdateById(Product product)
        {
            ctx.Entry(product).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
