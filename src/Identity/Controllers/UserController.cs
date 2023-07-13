using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Common.Models;
using Common.Repositories;
using Common.Core.Entities.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _userManager.Users.ToListAsync());
                ;
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