using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Category : BaseEntity
    {

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
