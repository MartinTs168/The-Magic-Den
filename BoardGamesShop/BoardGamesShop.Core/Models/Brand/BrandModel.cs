using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

namespace BoardGamesShop.Core.Models.Brand;

public class BrandModel
{
    public int Id {get; set;}
    
    [Required]
    [StringLength(BrandNameMaxLength, 
        MinimumLength = BrandNameMinLength, 
        ErrorMessage = "The field {0} must be between {2} and {1} chracters.")]
    public string Name { get; set; } = null!;
}