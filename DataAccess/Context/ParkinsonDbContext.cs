using System.IO;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public class ParkinsonContextFactory : IDesignTimeDbContextFactory<ParkinsonDbContext>
    {
        public ParkinsonDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ParkinsonDbContext>();
            optionsBuilder.UseMySQL(configuration.GetConnectionString("DefaultConnection"));

            return new ParkinsonDbContext(optionsBuilder.Options);
        }
    }
    
    public class ParkinsonDbContext : DbContext
    {
        public ParkinsonDbContext(DbContextOptions<ParkinsonDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Phone).IsUnique();
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Condition> Conditions { get; set; }
    }
}