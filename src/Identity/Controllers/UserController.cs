using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Common.Models;
using Common.Repositories;
using Common.Core.Entities.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Duende.IdentityServer.Models;

namespace Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _userManager.Users.ToListAsync());
                
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Claims(string user)
        {
            var User = await _userManager.FindByNameAsync(user);
            var claims = await _userManager.GetClaimsAsync(User);
            return Ok(claims);
                
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromForm]User User)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Form invalid";
                return View(User);
            }
            var user = await _userManager.FindByEmailAsync(User.Email);
            if (user != null)
            {
                ViewData["ErrorMessage"] = $"User with email {User.Email} already exist";
                return BadRequest( $"User with email {User.Email} already exist");
            }

            user = await _userManager.FindByNameAsync(User.UserName);
            if (user != null)
            {
                ViewData["ErrorMessage"] = $"User with UserName: {User.UserName} already exist";
                return BadRequest($"User with UserName: {User.UserName} already exist");
            }

            var result = await _userManager.CreateAsync(
                new ApplicationUser{
                    Email = User.Email,
                    UserName = User.UserName
                }, User.Password
            );
            user = await _userManager.FindByNameAsync(User.UserName);
            await _userManager.AddClaimAsync(user, new Claim (ClaimTypes.Role, "root"));
            _logger.LogInformation($"Added 'country' claim with value 'root' for user '{user.UserName}'.");
            var claims = await _userManager.GetClaimsAsync(user);
            System.Console.WriteLine("TEST");
            foreach (var item in claims)
            {
            _logger.LogInformation(item.Type+" - "+item.Value);

            }

          
            return Ok();

        }
      //  [HttpGet("{id}")]
      //  public async Task<IActionResult> GetByIdAsync(Guid id)
      //  {
      //      var user = await _usersRepository.GetUserAsync(id);
      //      if (user is null)
      //          return NotFound();
      //  
      //      return Ok(user);  
      //  }
      //  
      //  [HttpGet("{userName}")]
      //  public async Task<IActionResult> GetByNameAsync(string userName)
      //  {
      //      var user = await _usersRepository.GetUserAsync(userName);
      //      if (user is null)
      //          return NotFound();
      //  
      //      return Ok(user);  
      //  }

      //  [HttpPost]
      //  public async Task<IActionResult> CreateAsync([FromForm] User user)
      //  {
      //      await _usersRepository.CreateAsync(
      //          new ApplicationUser() { UserName = user.UserName, Email = user.Email },
      //          user.Password);
      //      return Ok(user);
      //  }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAsync(Guid id, UpdateItemDto updateItemDto)
        //{
        //    await _itemsRepository.UpdateAsync(id);
        //  return Ok();
        //}
    }

}