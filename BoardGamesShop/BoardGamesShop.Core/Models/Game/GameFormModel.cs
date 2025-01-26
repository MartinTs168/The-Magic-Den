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
    [Display(Name = "Image Url")]
    public string ImgUrl { get; set; } = null!;
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    [Display(Name = "Original Price")]
    [Range(typeof(decimal), GameMinOriginalPrice, GameMaxOriginalPrice)]
    public decimal OriginalPrice { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public int Discount { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public int Quantity { get; set; }
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    [StringLength(GameAgeRatingMaxLength, MinimumLength = GameAgeRatingMinLength,
        ErrorMessage = LengthErrorMessage)]
    [Display(Name = "Age Rating")]
    public string AgeRating { get; set; } = null!;

    [Required(ErrorMessage = RequiredErrorMessage)]
    [StringLength(GameNumberOfPlayersMaxLength, MinimumLength = GameNumberOfPlayersMinLength,
        ErrorMessage = LengthErrorMessage)]
    [Display(Name = "Number of players")]
    public string NumberOfPlayers { get; set; } = null!;
    
    [Required(ErrorMessage = RequiredErrorMessage)]
    public int? SubCategoryId{get; set; }

    public IEnumerable<GameSubCategoryServiceModel> SubCategories { get; set; } =
        new List<GameSubCategoryServiceModel>();
    
    [Required]
    public int BrandId { get; set; }
    
    public IEnumerable<BrandModel> Brands { get; set; } = new List<BrandModel>();
}