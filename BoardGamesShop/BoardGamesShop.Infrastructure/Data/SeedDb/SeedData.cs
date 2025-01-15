using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using static BoardGamesShop.Infrastructure.Constants.AdministratorConstants;

namespace BoardGamesShop.Infrastructure.Data.SeedDb;

public class SeedData
{
    public ApplicationUser AdminUser { get; set; }
    public ApplicationUser ClientUser { get; set; }
    
    public Category BoardGameCategory { get; set; }
    public Category CardGameCategory { get; set; }
    public Category RPGGameCategory { get; set; }
    
    public Brand SpaceCowboysBrand { get; set; }
    public Brand HasbroBrand { get; set; }

    public SeedData()
    {
        SeedCategories();
        SeedBrands();
    }

    private void SeedCategories()
    {
        BoardGameCategory = new Category()
        {
            Id = 1,
            Name = "Board Games"
        };
        
        CardGameCategory = new Category()
        {
            Id = 2,
            Name = "Card Games"
        };
        
        RPGGameCategory = new Category()
        {
            Id = 3,
            Name = "Role-Playing Games"
        };
    }

    private void SeedBrands()
    {
        HasbroBrand = new Brand()
        {
            Id = 1,
            Name = "Hasbro"
        };
        
        SpaceCowboysBrand = new Brand()
        {
            Id = 2,
            Name = "Space Cowboys"
        };
    }
}