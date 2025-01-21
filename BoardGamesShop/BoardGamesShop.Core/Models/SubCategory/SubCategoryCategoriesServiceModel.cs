using System.ComponentModel.DataAnnotations;

namespace BoardGamesShop.Core.Models.SubCategory;

public class SubCategoryCategoriesServiceModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}