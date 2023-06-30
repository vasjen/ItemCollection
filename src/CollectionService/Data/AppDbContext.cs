using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using CollectionService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CollectionService.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration _config;

        public DbSet<Item> Items {get;set;}
        public DbSet<Collection> Collections {get;set;}
        public DbSet<Comment> Comments {get;set;}
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                        .HasOne(c => c.Item)
                        .WithMany(i => i.Comments)
                        .HasForeignKey(c => c.ItemId)
                        .OnDelete(DeleteBehavior.NoAction);
                
            
                        
        }
}
}