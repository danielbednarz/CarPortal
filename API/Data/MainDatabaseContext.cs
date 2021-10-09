using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace API.Data
{
    public class MainDatabaseContext : DbContext
    {
        public MainDatabaseContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }

        // Add-Migration -o Data/Migrations Init
        // dotnet ef migrations add Init -o Data\Migrations
        // dotnet ef database update
    }
}
