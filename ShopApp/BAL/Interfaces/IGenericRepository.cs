using BLL.Spacification;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(ISpacification<TEntity> spec);
        Task<TEntity> GetbyIdAsync(ISpacification<TEntity> spec);

        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> values);


    }
}
