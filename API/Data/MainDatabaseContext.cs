using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Linq;

namespace API.Data
{
    public class MainDatabaseContext : DbContext
    {
        public MainDatabaseContext(DbContextOptions options) : base(options)
        {
            
        }

        // Add-Migration -o Data/Migrations Init
        // dotnet ef migrations add Init -o Data\Migrations
        // dotnet ef database update

        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<EnginesForModel> EnginesForModels { get; set; }
        public DbSet<FuelReport> FuelReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //modelBuilder.Entity<FuelReport>(entity =>
            //{
            //    entity.Property(e => e.Id).HasDefaultValueSql("NEWID()");
            //});
        }
    }
}
