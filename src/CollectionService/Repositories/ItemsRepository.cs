using System.Linq;
using CollectionService.Data;
using CollectionService.Models;
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

        public async Task<IEnumerable<T>> GetAllItemsAsync() => await _context.Set<T>().ToListAsync();

    }
}