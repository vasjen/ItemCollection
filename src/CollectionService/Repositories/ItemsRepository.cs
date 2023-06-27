using System.Linq.Expressions;
using CollectionService.Data;
using CollectionService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollectionService.Repositories
{
    public class ItemsRepository<T> : IRepository<T> where T: class,IEntity
     {
        private readonly AppDbContext _context;

        public ItemsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync() 
            => await _context.Set<T>().ToListAsync();

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
            => await _context.Set<T>().AsNoTracking().Where(filter).SingleAsync();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter) 
            =>  await _context.Set<T>().AsNoTracking().Where(filter).ToListAsync();
    
        public async Task<T> GetByIdAsync(Guid id)
            => await _context.Set<T>().AsNoTracking().Where(p => p.Id == id).SingleAsync();

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cann't be a null");

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await _context.Set<T>().AsNoTracking().Where(p => p.Id == id).SingleAsync();;
            if (entity == null)
                throw new NullReferenceException ($"{nameof(entity)} cann't be a null");

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id)
        {
            var entity = await _context.Set<T>().AsNoTracking().Where(p => p.Id == id).SingleAsync();;
            if (entity == null)
                throw new NullReferenceException ($"{nameof(entity)} cann't be a null");

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}