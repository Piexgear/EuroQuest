using Microsoft.EntityFrameworkCore;
using Euroquest.Models;

//kopplar user-modellen till databasen 
namespace Euroquest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //DbSet<User> skapar tabellen Users
        public DbSet<User> Users { get; set; }
    }
}
