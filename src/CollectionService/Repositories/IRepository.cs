using CollectionService.Models;

namespace CollectionService.Repositories
{
  public interface IRepository<T> where T: IEntity
    {
         Task<IEnumerable<T>> GetAllItemsAsync();
    }
}