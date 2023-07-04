using CollectionService.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CollectionService.Repositories
{
  public interface IUsersRepository<T> where T : IdentityUser<Guid>
    {
        Task CreateAsync(T user, string password);
        Task<bool> Login (T user, string password);
        AuthenticationResponse CreateToken(T user);
        Task<T> GetUserAsync(Guid id);
        Task<T> GetUserAsync(string name);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Guid id);
    }
}