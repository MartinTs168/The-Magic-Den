using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;
using static BoardGamesShop.Core.Constants.MessageConstants;

namespace BoardGamesShop.Core.Models.Brand;

public class BrandModel
{
    public int Id {get; set;}
    
    [Required]
    [StringLength(BrandNameMaxLength, 
        MinimumLength = BrandNameMinLength, 
        ErrorMessage = LengthErrorMessage)]
    public string Name { get; set; } = null!;
}