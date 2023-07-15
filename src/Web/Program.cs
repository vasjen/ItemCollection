using System.Security.Claims;
using System.Text;
using Common.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;
using Web.Data;
using static IdentityModel.OidcConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Personal");
    options.Conventions.AuthorizeFolder("/Admin","admin");
    
});
builder.Services.AddHttpClient("CollectionService", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["CollectionService"]);
});
builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "appcookie";
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
    {
        
        config.Authority = "https://localhost:7195";
        config.ClientId = "client_web_id";
        config.ClientSecret = "client_secret_web";
        config.SaveTokens = true;
        config.RequireHttpsMetadata = false;

        config.ResponseType = "code";
        config.GetClaimsFromUserInfoEndpoint = true;
        config.ClaimActions.MapAll();

        
        
    });
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("admin", policy =>
            policy.RequireClaim(ClaimTypes.Role, "Administrator"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy(new CookiePolicyOptions
{
    HttpOnly = HttpOnlyPolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
