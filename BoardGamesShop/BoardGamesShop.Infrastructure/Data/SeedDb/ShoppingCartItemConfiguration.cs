using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGamesShop.Infrastructure.Data.SeedDb;

public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder
            .HasKey(si => new { si.ShoppingCartId, si.GameId });
        
        builder
            .HasOne(si => si.ShoppingCart)
            .WithMany(sc => sc.ShoppingCartItems)
            .HasForeignKey(si => si.ShoppingCartId);
        
        builder
            .HasOne(si => si.Game)
            .WithMany(g => g.ShoppingCartItems)
            .HasForeignKey(si => si.GameId);
    }
}