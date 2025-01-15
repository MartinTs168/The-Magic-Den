using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BoardGamesShop.Infrastructure.Data.SeedDb;

public class SeedData
{
    public ApplicationUser AdminUser { get; set; }
    public ApplicationUser ClientUser { get; set; }

    public SeedData()
    {
        SeedUsers();
    }

    private void SeedUsers()
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        AdminUser = new ApplicationUser
        {
            FirstName = "Admin",
            LastName = "Adminov",
            UserName = "admin",
            Email = "admin@mail.com",
            Address = "London Street Admin 3",
            PhoneNumber = "0888123456",
            
        };
        
        AdminUser.PasswordHash = hasher.HashPassword(AdminUser, "admin123");

        ClientUser = new ApplicationUser
        {
            FirstName = "Client",
            LastName = "Clientov",
            UserName = "client",
            Email = "client@mail.com",
            Address = "Paris Street Client 1",
            PhoneNumber = "0999987654",
        };
        
        ClientUser.PasswordHash = hasher.HashPassword(ClientUser, "client123");
    }
}