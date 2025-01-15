using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGamesShop.Infrastructure.Data.SeedDb;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(u => u.Id)
            .HasDefaultValueSql("NEWID()");

        // var data = new SeedData();
        //
        // builder.HasData(new ApplicationUser[] { data.AdminUser, data.ClientUser });
    }
}