using System.Security.Claims;
using Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity
{
    public static class Seeding
    {
        public static async void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user =  new ApplicationUser
            {
                UserName = "vast",
                Email = "vast@gmail.com"

            };
            var result =  userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                System.Console.WriteLine("Created!!!!!");
                 userManager.AddClaimAsync(user, new System.Security.Claims.Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
            }
            else{
                System.Console.WriteLine("Not created!");
            }
        }
    }
}