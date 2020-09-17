using CentralDeErros.Model.Maps;
using CentralDeErros.Model.Models;
using Microsoft.EntityFrameworkCore;


namespace CentralDeErros.Core
{
    public class CentralDeErrosDbContext : DbContext
    {
        public DbSet<Microsservice> Microsservices { get; set; }
        public DbSet<Error> Errors{ get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Environment> Environments { get; set; }


        public CentralDeErrosDbContext(DbContextOptions<CentralDeErrosDbContext> options)
                  : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MicrosserviceMap());
            builder.ApplyConfiguration(new ErrorMap());
            builder.ApplyConfiguration(new LevelMap());
            builder.ApplyConfiguration(new EnvironmentMap());
        }
    }
}