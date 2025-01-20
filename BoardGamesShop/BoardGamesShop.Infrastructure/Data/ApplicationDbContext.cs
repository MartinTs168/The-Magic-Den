using BoardGamesShop.Infrastructure.Data.Entities;
using BoardGamesShop.Infrastructure.Data.SeedDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Brand>? Brands { get; set;}
        public DbSet<Game>? Games { get; set; }
        public DbSet<Order>? Orders { get; set; } // Is not necessary since the relationship is defined in game configuration
        public DbSet<SubCategory> SubCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            
            base.OnModelCreating(modelBuilder);

        }
    }
}
