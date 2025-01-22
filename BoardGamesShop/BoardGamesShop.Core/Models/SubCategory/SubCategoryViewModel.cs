using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;
using static BoardGamesShop.Core.Constants.MessageConstants;

namespace BoardGamesShop.Core.Models.SubCategory;

public class SubCategoryViewModel
{
    public int Id {get; set; }
    
    [Required]
    [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, 
        ErrorMessage = LengthErrorMessage)]
    public string Name { get; set; } = null!;
    
    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public IEnumerable<SubCategoryCategoriesServiceModel> Categories { get; set; } = 
        new List<SubCategoryCategoriesServiceModel>();

}