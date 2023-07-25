using Microsoft.AspNetCore.Mvc;
using Common.Core.Entities;
using Common.Repositories;
using Common.Core.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Duende.IdentityServer.Services;

namespace Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interactionService
        )
        {
            _userManager = userManager;   
            _signInManager = signInManager;
            _interactionService = interactionService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            
            return View();
        }
      
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] AuthenticationRequest request)
        {
            System.Console.WriteLine(request.ReturnUrl);
           
            if (!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Form invalid";
                return View(request);
            }

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                ViewData["ErrorMessage"] = $"User {request.UserName} is doesn't exist";
                return View(request);
            }

            var correctCredentials = await _signInManager.PasswordSignInAsync(user,request.Password,false,false);
            
            if (correctCredentials.Succeeded)
            {   
                System.Console.WriteLine("Success!");
                return Redirect(request.ReturnUrl);
            }
            else{
                ViewData["ErrorMessage"] = $"Incorrect credentials";
            ModelState.AddModelError($"User {request.UserName}", "Oooops");
            }
            return View(request);

        }

        [Route("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutResult = await _interactionService.GetLogoutContextAsync(logoutId);
            if (String.IsNullOrEmpty(logoutResult.PostLogoutRedirectUri))
            {
                return RedirectToPage("Index");
            }

            return Redirect(logoutResult.PostLogoutRedirectUri);
        }




    
     
}
}