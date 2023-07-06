using System.Linq.Expressions;
using Common.Core.Entities;

namespace CollectionService.Repositories
{
  public interface IRepository<T> where T: IEntity
    {
         Task<IEnumerable<T>> GetAllAsync();
         Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> filter);
         Task<T> GetByIdAsync(Guid id);
         Task<T> GetAsync(Expression<Func<T,bool>> filter);
         Task CreateAsync(T entity);
         Task RemoveAsync(Guid id);
         Task UpdateAsync(Guid id);
    }
}