using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;
using static BoardGamesShop.Core.Constants.MessageConstants;

namespace BoardGamesShop.Core.Models.Game;

public class GameServiceModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = RequiredErrorMessage)]
    [StringLength(GameNameMaxLength, MinimumLength = GameNameMinLength, ErrorMessage = LengthErrorMessage)]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public string ImgUrl { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    [Display(Name = "Original Price")]
    public decimal OriginalPrice { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public int Discount { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public bool IsInStock { get; set; }
    
    [Display(Name = "Category")]
    public string? SubCategoryName { get; set; }

    [Required(ErrorMessage = RequiredErrorMessage)]
    [Display(Name = "Brand")]
    public string BrandName { get; set; } = null!;
}