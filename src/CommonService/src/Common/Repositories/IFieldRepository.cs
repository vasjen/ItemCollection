using System.Linq.Expressions;
using Common.Core.Entities;
using Common.Models;

namespace Common.Repositories
{
  public interface IFieldRepository<T> where T: Field
    {
         Task<IEnumerable<T>> GetAllAsync();
         Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> filter);
         Task<T> GetAsync(Expression<Func<T,bool>> filter);
         Task CreateAsync(T entity);
         Task<T> GetByIdAsync(long id);
    }
}