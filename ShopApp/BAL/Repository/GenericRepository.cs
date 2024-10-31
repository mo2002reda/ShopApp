using BLL.Interfaces;
using BLL.Spacification;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;


        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task AddAsync(TEntity entity)
        => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
         => _context.Set<TEntity>().Remove(entity);

        public void DeleteRange(IEnumerable<TEntity> values)
        => _context.Set<TEntity>().RemoveRange(values);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpacification<TEntity> spec)
        => await ApplySpacification(spec).ToListAsync();

        public async Task<TEntity> GetbyIdAsync(ISpacification<TEntity> spec)
        => await ApplySpacification(spec).FirstOrDefaultAsync();

        private IQueryable<TEntity> ApplySpacification(ISpacification<TEntity> spec)
        => SpacificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>(), spec);
    }
}
