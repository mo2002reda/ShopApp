using BLL.Repository;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUniteOfWork : IDisposable
    {
        public ICategory Category { get; set; }
        public IProduct Product { get; set; }
        public Task<int> CompleteAsync();
    }
}
