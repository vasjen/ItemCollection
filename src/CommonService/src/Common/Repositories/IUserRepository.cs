
using Common.Core.Entities.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Common.Repositories
{
  public interface IUserRepository<T> where T : IdentityUser<Guid>
    {
        Task CreateAsync(T user, string password);
        Task<bool> ValidateCredentials (T user, string password);
        AuthenticationResponse CreateToken(T user);
        Task<T> GetUserAsync(Guid id);
        Task<T> GetUserAsync(string name);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Guid id);
        Task SignIn(T user);
        Task<IEnumerable<T>> GetAllUsers();
    }
}