using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("Yarp"));

builder.Services.AddAuthentication("cookie")
        .AddCookie("cookie");
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


    app.UseEndpoints(endpoints =>
    {
        endpoints.MapReverseProxy();
    });

app.Run();
