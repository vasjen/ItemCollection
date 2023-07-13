using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using Common.Core.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Common.Repositories
{
    

    public class UserRepository<T> : IUserRepository<T> 
        where T : IdentityUser<Guid>
    {
        private const int EXPIRATION_MINUTES = 5000;
        private readonly UserManager<T> _manager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<T> _signInManager;

        public UserRepository(UserManager<T> manager, IConfiguration configuration, SignInManager<T> signInManager)
        {
            _manager = manager;
            _configuration = configuration;
            _signInManager = signInManager;
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

        public async Task<bool> ValidateCredentials(T user, string password)
            => await _manager.CheckPasswordAsync(user, password);

        public AuthenticationResponse CreateToken(T user)
        {
             var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);

            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return new AuthenticationResponse {
                Token = tokenHandler.WriteToken(token),
                Expiration = expiration
            };
        }

        private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
            new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
            );
           private Claim[] CreateClaims(T user) =>
            new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

        private SigningCredentials CreateSigningCredentials() =>
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                ),
                SecurityAlgorithms.HmacSha256
            );

        public async Task SignIn(T user)
        {
             await _signInManager.SignInAsync(user,true);
        }

        public async Task<IEnumerable<T>> GetAllUsers()
        {
            return await _manager.Users.ToListAsync();
        }
    }
}