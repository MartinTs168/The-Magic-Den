using BoardGamesShop.Infrastructure.Data.Entities;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Game>()
                .HasMany(e => e.Orders)
                .WithMany(e => e.Games)
                .UsingEntity<GameOrder>();

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
