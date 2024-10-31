using BLL.Interfaces;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategory
    {
        private readonly ApplicationDbContext _context;


        public CategoryRepository(ApplicationDbContext _Context) : base(_Context)
        {
            _context = _Context;

        }


        public void UpdateAsync(Category category)
        {
            var Category = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (Category != null)
            {
                Category.Name = category.Name;
                Category.Description = category.Description;
                Category.CreateDate = DateTime.Now;
            }


        }

    }
}
