using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using CollectionService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CollectionService.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Item> Items {get;set;}
        public DbSet<Tag> Tags {get;set;}
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
        : base(options)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
}
}