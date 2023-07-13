
using Microsoft.EntityFrameworkCore;
using CollectionService.Data;
using CollectionService.Repositories;
using CollectionService.Services;
using CollectionService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DbConnection");
            options.UseSqlServer(connection, b =>
                b.MigrationsAssembly("CollectionService"));
});
builder.Services.AddCustomAuth();
builder.Services.AddScoped(typeof(IRepository<>), typeof(ItemsRepository<>));
//builder.Services.AddScoped<ITokenCreationService, JwtService>();
//builder.Services.AddScoped(typeof(IUsersRepository<>), typeof(UsersRepository<>));
// builder.Services.AddOldIdentity(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
};
app.Run();
