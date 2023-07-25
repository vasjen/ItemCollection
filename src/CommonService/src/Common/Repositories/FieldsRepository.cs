using System.Linq.Expressions;
using Common.Core.Entities;
using Common.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Common.Repositories
{
    public class FieldsRepository<T> : IFieldRepository<T> where T: Field
     {
        private readonly AppDbContext _context;

        public FieldsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync() 
            => await _context.Set<T>().ToListAsync();

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
            => await _context.Set<T>().AsNoTracking().Where(filter).SingleAsync();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter) 
            =>  await _context.Set<T>().AsNoTracking().Where(filter).ToListAsync();
    
        public async Task<T> GetByIdAsync(long id)
            => await _context.Set<T>().AsNoTracking().Where(p => p.Id == id).SingleAsync();

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} cann't be a null");

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

       
     
    }
}