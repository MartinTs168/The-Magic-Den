using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

namespace BoardGamesShop.Core.Models.SubCategory;

public class SubCategoryViewModel
{
    public int Id {get; set; }
    
    [Required]
    [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, 
        ErrorMessage = "The field {0} must be between {2} and {1} characters long.")]
    public string Name { get; set; } = null!;
    
    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public IEnumerable<SubCategoryCategoriesServiceModel> Categories { get; set; } = 
        new List<SubCategoryCategoriesServiceModel>();

}