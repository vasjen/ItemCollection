using System.Linq.Expressions;
using CollectionService.Data;
using CollectionService.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollectionService.Repositories
{
    

    public class UsersRepository<T> : IUsersRepository<T> 
        where T : IdentityUser<Guid>
    {
        private readonly UserManager<T> _manager;

        public UsersRepository(UserManager<T> manager)
        {
            _manager = manager;
        }

        public async Task<T> GetUserAsync(Guid id)
            => await _manager.FindByIdAsync(id.ToString());

        public async Task<T> GetUserAsync(string userName)
            => await _manager.FindByNameAsync(userName);


        public async Task CreateAsync(T user, string pass)
        {
            if (user == null)
                throw new ArgumentNullException($"{nameof(user)} cann't be a null");

            await _manager.CreateAsync(user, pass);
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await _manager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new NullReferenceException($"{nameof(user)} cann't be a null");

            _manager.DeleteAsync(user);
        }

        public async Task UpdateAsync(Guid id)
        {
            var user = await _manager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new NullReferenceException($"{nameof(user)} cann't be a null");

            await _manager.UpdateAsync(user);
        }

       
    }
}