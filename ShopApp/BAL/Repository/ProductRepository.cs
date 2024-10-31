using BLL.Interfaces;
using DAL.Context;
using DAL.Entities;

namespace BLL.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateAsync(Product product)
        {
            var Product = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (Product != null)
            {
                Product.Name = product.Name;
                Product.Description = product.Description;
                Product.Price = product.Price;
                Product.Img = product.Img;
            }


        }
    }
}
