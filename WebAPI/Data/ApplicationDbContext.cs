using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Shirt> Shirts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //data seeding
            modelBuilder.Entity<Shirt>().HasData(
                new Shirt { ShirtId = 1, Brand = "My Brand", Color = "Blue", Gender = "Men", Price = 30, Size = 10 },
                new Shirt { ShirtId = 2, Brand = "My Brand", Color = "Black", Gender = "Men", Price = 35, Size = 12 },
                new Shirt { ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender = "Women", Price = 28, Size = 8 },
                new Shirt { ShirtId = 4, Brand = "Your Brand", Color = "Yello", Gender = "Women", Price = 30, Size = 9 }
            );
        }
    }
}

//https://medium.com/@vndpal/how-to-connect-net-web-api-with-sql-server-using-entity-framework-code-first-approach-8564192485c9

//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//dotnet add package Microsoft.EntityFrameworkCore.Tools
//dotnet add package Microsoft.EntityFrameworkCore.Design

// add-migration
// update-database