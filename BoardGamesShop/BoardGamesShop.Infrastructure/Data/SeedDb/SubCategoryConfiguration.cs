using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGamesShop.Infrastructure.Data.SeedDb;

public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.HasIndex(sc => sc.Name)
            .IsUnique();
    }
}