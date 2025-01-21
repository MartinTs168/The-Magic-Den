using BoardGamesShop.Core.Models.Category;

namespace BoardGamesShop.Core.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<AllCategoriesViewModel>> AllAsync();

    Task<int> CreateAsync(CategoryFormViewModel model);

    Task EditAsync(CategoryFormViewModel model, int id);
    
    Task<CategoryFormViewModel?> GetByIdAsync(int id);

    Task DeleteAsync(int id);
}