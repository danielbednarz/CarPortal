using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<AppUser> Users { get; set; }

        // Add-Migration -o Data/Migrations Init
        // dotnet ef migrations add Init -o Data\Migrations
        // dotnet ef database update
    }
}
