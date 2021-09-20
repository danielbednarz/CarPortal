using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MainDatabaseContext : DbContext
    {
        public MainDatabaseContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        // Add-Migration -o Data/Migrations Init
        // dotnet ef migrations add Init -o Data\Migrations
        // dotnet ef database update
    }
}
