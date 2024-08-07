using ApiTest.Services;
using Moq;
using WebApplication1.Domains;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;
namespace ApiTest
{
    public class ProductsTestWithoutMock : IClassFixture<DbContextFixture>
    {

        private readonly DbContextFixture dbCtx;
        private readonly ProductRepository repository;

        public ProductsTestWithoutMock(DbContextFixture fixture)
        {
            dbCtx = fixture;
            repository = new ProductRepository(dbCtx.ctx);
        }

        [Fact]
        public void Get_ListsAllProducts()
        {
            var products = new List<Product>
            {
                new Product{Id = Guid.NewGuid(), Name = "Banana", Price = 3.5M},
                new Product{Id = Guid.NewGuid(), Name = "Batata", Price = 1.5M},
                new Product{Id = Guid.NewGuid(), Name = "Beterraba", Price = 5.5M},
            };

            var mockRepository = new Mock<IProductRepository>();

            mockRepository.Setup(x => x.GetAll()).Returns(products);

            var result = mockRepository.Object.GetAll();

            Assert.Equal(3, result.Count);
        }

        [Fact]

        public void Create_AddsNewProductToDatabase()
        {

            var product = new Product { Name = "Banana", Price = 3.5M };

            repository.Create(product);

            Assert.Equal(3, dbCtx.ctx.Products.Count());
            var addedProduct = dbCtx.ctx.Products.First();
            Assert.NotNull(addedProduct);
            Assert.Equal(product.Name, addedProduct.Name);
            Assert.Equal(product.Price, addedProduct.Price);

        }


        [Fact]
        public void DeleteByID_DeletesAProductFromDatabase()
        {

            var product = new Product { Name = "Banana", Price = 3.5M };
            repository.Create(product);

            var anteriorCount = dbCtx.ctx.Products.Count();
            var addedProduct = dbCtx.ctx.Products.First();

            repository.DeleteById(addedProduct.Id);

            Assert.Equal(0, dbCtx.ctx.Products.Count());
            var deletedProduct = dbCtx.ctx.Products.Find(addedProduct.Id);
            Assert.Null(deletedProduct);

        }

        [Fact]
        public void GetByID_GetsTheRightProductFromDatabase()
        {

            var product = new Product { Name = "Banana", Price = 3.5M };
            var product2 = new Product { Name = "Abacate", Price = 3.5M };

            repository.Create(product);
            repository.Create(product2);

            var firstProduct = dbCtx.ctx.Products.First();

            var _product = repository.GetById(firstProduct.Id);

            Assert.NotNull(_product);
            Assert.Equal(_product.Price, firstProduct.Price);
            Assert.Equal(_product.Name, firstProduct.Name);

        }
    }
}