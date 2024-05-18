using BulkyBook.Models;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Category> categories { get; set; }
        public DbSet<CoverType> coverTypes { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
