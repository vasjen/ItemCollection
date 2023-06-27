using System.Linq.Expressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common.EFCore;

using Common.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

public static class Extensions
{
    public static IServiceCollection AddCustomDbContext<TContext>(
        this IServiceCollection services, IConfiguration configuration)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var connection = configuration.GetConnectionString("DbConnection");
            options.UseSqlServer(connection);
        });
        services.AddScoped(typeof(IRepository<>), typeof(ItemsRepository<>));
        return services;
    }


   
}
