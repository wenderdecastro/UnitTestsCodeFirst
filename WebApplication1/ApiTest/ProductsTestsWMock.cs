using Microsoft.EntityFrameworkCore;
using Moq;
using WebApplication1.Contexts;
using WebApplication1.Domains;
using WebApplication1.Repositories;

namespace ApiTest
{
    public class ProductsTestsWMock
    {
        [Fact]
        public void Create_AddAProductToDB()
        {
            var mockSet = new Mock<DbSet<Product>>();
            var mockCtx = new Mock<ProductDbContext>();
            mockCtx.Setup(x => x.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockCtx.Object);

            var product = new Product { Name = "Banana", Price = 3.5M };

            repository.Create(product);

            mockSet.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
            mockCtx.Verify(x => x.SaveChanges(), Times.Once);

        }

        [Fact]
        public void GetById_ReturnsProduct()
        {
            var mockSet = new Mock<DbSet<Product>>();
            var mockCtx = new Mock<ProductDbContext>();
            mockCtx.Setup(x => x.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockCtx.Object);

            var id = Guid.NewGuid();
            var product = new Product { Id = id, Name = "Banana", Price = 3.5M };

            mockSet.Setup(m => m.Find(It.IsAny<Guid>())).Returns(product);

            var result = repository.GetById(id);

            Assert.NotNull(result);
            Assert.Equal("Banana", result.Name);
        }

        [Fact]
        public void DeleteById_RemovesProduct()
        {
            var mockSet = new Mock<DbSet<Product>>();
            var mockCtx = new Mock<ProductDbContext>();
            mockCtx.Setup(x => x.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockCtx.Object);

            var id = Guid.NewGuid();
            var product = new Product { Id = id, Name = "Banana", Price = 3.5M };

            mockSet.Setup(m => m.Find(It.IsAny<Guid>())).Returns(product);

            repository.DeleteById(id);

            mockSet.Verify(x => x.Remove(It.IsAny<Product>()), Times.Once);
            mockCtx.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnsAllProducts()
        {

            var data = new List<Product>
            {
                new Product { Name = "Banana", Price = 3.5M },
                new Product { Name = "Maça", Price = 2.0M }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockCtx = new Mock<ProductDbContext>();
            mockCtx.Setup(x => x.Products).Returns(mockSet.Object);

            var repository = new ProductRepository(mockCtx.Object);

            var products = repository.GetAll();

            Assert.Equal(2, products.Count);
            Assert.Equal("Banana", products[0].Name);
            Assert.Equal("Maça", products[1].Name);

        }
    }
}
