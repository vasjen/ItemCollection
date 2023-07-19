
using Microsoft.EntityFrameworkCore;
using CollectionService.Extensions;
using CollectionService;
using Common.Repositories;
using Common.EFCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddCustomAuth();

builder.Services.AddScoped(typeof(IFieldRepository<>),typeof(FieldsRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(ItemsRepository<>));
builder.Services.AddScoped<FieldCreationService>();
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
