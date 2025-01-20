using BoardGamesShop.Core.Models.SubCategory;

namespace BoardGamesShop.Core.Models.Category;

public class AllCategoriesViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<SubCategoryViewModel> SubCategories { get; set; } = new List<SubCategoryViewModel>();
}