using Common.Models;
using Identity;
using Identity.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DbConnection");
            options.UseSqlServer(connection);
});
builder.Services
.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
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
            .AddSignInManager<SignInManager<ApplicationUser>>();;
// Add services to the container.
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
    
});
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryClients(Configuration.GetClients())
    .AddInMemoryApiScopes(Configuration.GetApiScopes())
    .AddInMemoryApiResources(Configuration.GetApiResources())
    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    .AddProfileService<ProfileService>()
    .AddDeveloperSigningCredential();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo("/app/DataProtection-Keys"));  
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseIdentityServer();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
    
}
app.Run();
