using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IProduct : IGenericRepository<Product>
    {
        void UpdateAsync(Product product);
    }
}
