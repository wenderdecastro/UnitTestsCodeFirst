using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Domains
{
    public class Product
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
