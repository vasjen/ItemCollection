using System.Security.Claims;
using System.Text;
using Identity;
using Identity.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.BuildAndSetup();

var app = builder.Build();
app.MapGet("/",() => $"Wellcome to identity").AllowAnonymous();
app.MapGet("/auth",() => $"Wellcome to auth").AllowAnonymous();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
