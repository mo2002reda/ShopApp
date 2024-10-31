using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Img { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
