using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Common.Core.Entities;
using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Common.EFCore
{
    
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Item> Items {get;set;}
        public DbSet<Collection> Collections {get;set;}
        public DbSet<Comment> Comments {get;set;}
        public DbSet<Tag> Tags {get;set;}
        public DbSet<Fields> Fields {get;set;}
        // public DbSet<Field> BaseFields {get;set;}
        public DbSet<FieldInt> FieldInt {get;set;}
        public DbSet<FieldString> FieldString {get;set;}
        public DbSet<FieldText> FieldText {get;set;}
        public DbSet<FieldBool> FieldBool {get;set;}
        public DbSet<FieldDate> FieldDate {get;set;}
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
        : base(options)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = _config.GetConnectionString("DbConnection");
            optionsBuilder.UseSqlServer(connection, b =>
                b.MigrationsAssembly("CollectionService"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //  modelBuilder.Entity<Field>()
            //.HasDiscriminator<string>("FieldType")
            //.HasValue<FieldInt>("FieldInt")
            //.HasValue<FieldString>("FieldString")
            //.HasValue<FieldText>("FieldText")
            //.HasValue<FieldBool>("FieldBool")
            //.HasValue<FieldDate>("FieldDate")
            //.HasValue<FieldTime>("FieldTime");


            modelBuilder.Ignore<ApplicationUser>();
            modelBuilder.Entity<Comment>()
                        .HasOne(c => c.Item)
                        .WithMany(i => i.Comments)
                        .HasForeignKey(c => c.ItemId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Collection>()
                        .HasMany(c => c.Items)        
                        .WithOne(i => i.Collection)  
                        .HasForeignKey(i => i.CollectionId);

           



           


            //modelBuilder.Entity<Fields>()
            //.HasOne(f => f.Collection)
            //.WithOne(c => c.Fields)
            //.HasForeignKey<Fields>(f => f.Id); 

            
            
                        
        }
}
}