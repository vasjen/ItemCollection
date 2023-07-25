using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Common.Core.Entities;
using Common.Repositories;
using Common.Core.Entities.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IIdentityServerInteractionService _interactionService;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger, IIdentityServerInteractionService interactionService)
        {
            _userManager = userManager;
            _logger = logger;
            _interactionService = interactionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _userManager.Users.ToListAsync());
                
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
            return Ok();

        }

         [Authorize]
         [HttpPut("Block")]
         public async Task<IActionResult> BlockAsync([FromBody] string[] IdList)
         {
             foreach (var item in IdList)
             {
                 var user = await _userManager.FindByIdAsync($"{item}");
                 if (user is not null)
                 {
                     user.IsActive = false;
                     await _userManager.UpdateAsync(user);
                 }
             }
             return RedirectToAction("Table","Home");
         }


         [Authorize]
         [HttpPut("Activate")]
         public async Task<IActionResult> ActivateSync([FromBody] string[] IdList)
         {
             _logger.LogInformation("\n\n!!!!!!!Get Activate request\n\n");
             foreach (var item in IdList)
             {
                 var user = await _userManager.FindByIdAsync($"{item}");
                 if (user is not null)
                 {
                     user.IsActive = true;
                     await _userManager.UpdateAsync(user);
                 }
             }
            return RedirectToAction("Table","Home");
         }

         [Authorize]
         [HttpDelete("Delete")]
         public async Task<IActionResult> DeleteAsync([FromBody] string[] IdList)
         {
             foreach (var item in IdList)
             {
                 var user = await _userManager.FindByIdAsync($"{item}");
                 if (user is not null)
                 {
                    await _interactionService.GetLogoutContextAsync(item);
                     await _userManager.DeleteAsync(user);
                 }
             }
             return RedirectToAction("Table","Home");
         }
          
       }

}