using WebApplication1.Domains;

namespace WebApplication1.Interfaces
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product GetById(Guid id);
        public Product Create(Product products);
        public void DeleteById(Guid id);
        public void UpdateById(Product products);

    }
}
