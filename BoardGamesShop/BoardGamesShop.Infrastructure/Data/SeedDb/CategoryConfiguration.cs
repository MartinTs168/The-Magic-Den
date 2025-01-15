using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGamesShop.Infrastructure.Data.SeedDb;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(c => c.Name)
            .IsUnique();
        
        var data = new SeedData();
        
        builder.HasData(new Category[] { data.BoardGameCategory, data.CardGameCategory, data.RPGGameCategory });
    }
}