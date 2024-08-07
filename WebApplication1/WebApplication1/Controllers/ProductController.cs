using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domains;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }


        [HttpGet]
        public ActionResult GetAll() => Ok(repository.GetAll());

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {

            try
            {
                var content = repository.GetById(id);
                return Ok(content);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return NotFound();
                throw;
            }

        }

        [HttpDelete]
        public ActionResult DeleteById(Guid id)
        {
            try
            {
                repository.DeleteById(id);
                return NoContent();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return NotFound();
                throw;
            }
        }


        [HttpPost]
        public ActionResult Create(Product product)
        {

            try
            {
                var newProduct = repository.Create(product);
                return StatusCode(201, newProduct);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }


        [HttpPatch("{id}")]
        public ActionResult Update(Product product)
        {

            try
            {
                if (repository.GetById(product.Id) == null)
                {
                    return NotFound();
                }
                repository.UpdateById(product);
                return Ok();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return NotFound();
                throw;
            }

        }


    }
}
