using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BoardGamesShop.Infrastructure.Data.SeedDb;

public class SeedData
{
    public ApplicationUser AdminUser { get; set; }

    public SeedData()
    {
        SeedUsers();
    }

    private void SeedUsers()
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        
    }
}