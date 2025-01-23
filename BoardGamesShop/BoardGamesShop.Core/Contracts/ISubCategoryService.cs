using BoardGamesShop.Core.Models.Game;
using BoardGamesShop.Core.Models.SubCategory;

namespace BoardGamesShop.Core.Contracts;

public interface ISubCategoryService
{

    Task<IEnumerable<GameSubCategoryServiceModel>> AllAsync();
    
    Task<int> CreateAsync(SubCategoryViewModel model);
    
    Task<SubCategoryViewModel?> GetByIdAsync(int id);
    
    Task EditAsync(SubCategoryViewModel model, int id);
    
    Task DeleteAsync(int id);
    
    Task<bool> CategoryExistsAsync(int categoryId);

    Task<IEnumerable<SubCategoryCategoriesServiceModel>> AllCategoriesAsync();
}