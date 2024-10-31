using BLL.Interfaces;
using DAL.Context;

namespace BLL.Repository
{
    public class UniteOfWorkRepository : IUniteOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICategory Category { get; set; }
        public IProduct Product { get; set; }
        public UniteOfWorkRepository(ApplicationDbContext context)
        {
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            _context = context;
        }


        public async Task<int> CompleteAsync()
         => await _context.SaveChangesAsync();

        public void Dispose()
         => _context.Dispose();
    }
}
