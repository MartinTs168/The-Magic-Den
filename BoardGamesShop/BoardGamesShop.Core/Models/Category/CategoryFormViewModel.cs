using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;
using static BoardGamesShop.Core.Constants.MessageConstants;

namespace BoardGamesShop.Core.Models.Category;

public class CategoryFormViewModel
{
    
    public int Id { get; set; }
    
    [Required]
    [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength,
        ErrorMessage = LengthErrorMessage)]
    public string Name { get; set; } = null!;
}