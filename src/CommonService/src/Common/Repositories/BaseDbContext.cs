using Common.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Common.Repositories
{
    public class BaseDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public IEntity Items {get;set;}
        public BaseDbContext(DbContextOptions<BaseDbContext> options, IConfiguration config)
        : base(options)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}