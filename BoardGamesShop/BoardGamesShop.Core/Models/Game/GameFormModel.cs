using System.ComponentModel.DataAnnotations;
using BoardGamesShop.Core.Models.Brand;
using static BoardGamesShop.Core.Constants.MessageConstants;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

namespace BoardGamesShop.Core.Models.Game;

public class GameFormModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = RequiredErrorMessage)]
    [StringLength(GameNameMaxLength, MinimumLength = GameNameMinLength, ErrorMessage = LengthErrorMessage)]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public string ImgUrl { get; set; } = null!;
    
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
    public int NumberOfPlayers { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public int SubCategoryId{get; set; }

    public IEnumerable<GameSubCategoryServiceModel> SubCategories { get; set; } =
        new List<GameSubCategoryServiceModel>();
    
    [Required]
    public int BrandId { get; set; }
    
    public IEnumerable<BrandModel> Brands { get; set; } = new List<BrandModel>();
}