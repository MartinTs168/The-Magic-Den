using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;
using static BoardGamesShop.Core.Constants.MessageConstants;

namespace BoardGamesShop.Core.Models.Game;

public class GameFullDetailsViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = RequiredErrorMessage)]
    [StringLength(GameNameMaxLength, MinimumLength = GameNameMinLength, ErrorMessage = LengthErrorMessage)]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public string ImgUrl { get; set; } = null!;
    
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public decimal OriginalPrice { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public int Discount { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public int Quantity { get; set; }

    [Required(ErrorMessage = RequiredErrorMessage)]
    [StringLength(GameAgeRatingMaxLength, MinimumLength = GameAgeRatingMinLength,
        ErrorMessage = LengthErrorMessage)]
    public string AgeRating { get; set; } = null!;

    [Required(ErrorMessage = RequiredErrorMessage)]
    public string NumberOfPlayers { get; set; } = null!;
    
    [Display(Name = "Category")]
    public string? SubCategoryName { get; set; }

    [Required(ErrorMessage = RequiredErrorMessage)]
    [Display(Name = "Brand")]
    public string BrandName { get; set; } = null!;
}