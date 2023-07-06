using System.Security.Claims;
using Common.Models;
using Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public static class Extension
    {
        public static WebApplicationBuilder BuildAndSetup(this WebApplicationBuilder builder)
        {
            
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("DbConnection");
                        options.UseSqlServer(connection);
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<SignInManager<ApplicationUser>>();

            builder.Services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.Name = "test";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(24); 
                    options.SlidingExpiration = true;
                });
            builder.Services.AddAuthorization();
            return builder;
        }
        
    }
}