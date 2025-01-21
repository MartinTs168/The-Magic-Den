using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGamesShop.Infrastructure.Data.SeedDb;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder
            .HasMany(e => e.Orders)
            .WithMany(e => e.Games)
            .UsingEntity<GameOrder>();

        builder
            .HasOne(g => g.SubCategory)
            .WithMany(sc => sc.Games)
            .OnDelete(DeleteBehavior.SetNull);

    }
}