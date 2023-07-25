

using System.Text;
using Common.EFCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CollectionService.Extensions
{
    public static class CustomIdentity
    {
      
        public static IServiceCollection AddCustomAuth(this IServiceCollection Services)
        {
            
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                
                options.Authority = "https://localhost:7195";
                options.Audience = "CollectionApi";
                options.RequireHttpsMetadata = false;
            });

            return Services;
        }
    }
}