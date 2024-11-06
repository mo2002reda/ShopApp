using DAL.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Shop_web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage ="Description Is Required")]
        public string Description { get; set; }
        [ValidateNever]
        public string ImgName { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category category { get; set; }

    }
}

