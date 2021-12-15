using API.Entities;
using API.Entities.Views;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Data
{
    public class MainDatabaseContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public MainDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        // Add-Migration -o Data/Migrations Init
        // dotnet ef migrations add Init -o Data\Migrations
        // dotnet ef database update

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<EnginesForModel> EnginesForModels { get; set; }
        public DbSet<FuelReport> FuelReports { get; set; }
        public DbSet<RepairReport> RepairReports { get; set; }
        public DbSet<FuelReportView> FuelReportView { get; set; }
        public DbSet<RepairReportView> RepairReportView { get; set; }
        public DbSet<TotalCostsReportView> TotalCostsReportView { get; set; }
        public DbSet<TotalRepairFuelCostsReportView> TotalRepairFuelCostsReportView { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<CarInsurance> CarInsurances { get; set; }
        public DbSet<CarInsuranceRemainingDays> CarInsuranceRemainingDays { get; set; }
        public DbSet<PeriodicInspection> PeriodicInspections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(x => x.UserRoles)
                .WithOne(y => y.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(x => x.UserRoles)
                .WithOne(y => y.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Recipient)
                .WithMany(m => m.MessagesReceived);

            modelBuilder.Entity<Note>()
                .HasOne(x => x.User)
                .WithMany(y => y.Notes);

            modelBuilder.Entity<CarInsurance>()
                .HasOne(x => x.User)
                .WithMany(y => y.CarInsurances);

            modelBuilder.Entity<PeriodicInspection>()
                .HasOne(x => x.User)
                .WithMany(y => y.PeriodicInspections);

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany(m => m.MessagesSent);


            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.Entity<FuelReportView>().HasNoKey().ToView("FuelReportView");
            modelBuilder.Entity<RepairReportView>().HasNoKey().ToView("RepairReportView");
            modelBuilder.Entity<TotalCostsReportView>().HasNoKey().ToView("TotalCostsReportView");
            modelBuilder.Entity<TotalRepairFuelCostsReportView>().HasNoKey().ToView("TotalRepairFuelCostsReportView");

            modelBuilder.Entity<CarInsuranceRemainingDays>().HasNoKey();
        }
    }
}
