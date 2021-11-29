using API.Entities;
using API.Entities.Views;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<RepairReport> RepairReports { get; set; }
        public DbSet<FuelReportView> FuelReportView { get; set; }
        public DbSet<RepairReportView> RepairReportView { get; set; }
        public DbSet<TotalCostsReportView> TotalCostsReportView { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
            .HasOne(x => x.Recipient)
            .WithMany(m => m.MessagesReceived);

            modelBuilder.Entity<Message>()
            .HasOne(x => x.Sender)
            .WithMany(m => m.MessagesSent);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<FuelReportView>().HasNoKey().ToView("FuelReportView");
            modelBuilder.Entity<RepairReportView>().HasNoKey().ToView("RepairReportView");
            modelBuilder.Entity<TotalCostsReportView>().HasNoKey().ToView("TotalCostsReportView");

        }
    }
}
