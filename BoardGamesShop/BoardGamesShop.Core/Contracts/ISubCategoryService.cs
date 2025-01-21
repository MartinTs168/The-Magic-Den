using BoardGamesShop.Core.Models.SubCategory;

namespace BoardGamesShop.Core.Contracts;

public interface ISubCategoryService
{
    Task<int> CreateAsync(SubCategoryViewModel model);
    
    Task<SubCategoryViewModel?> GetByIdAsync(int id);
    
    Task EditAsync(SubCategoryViewModel model, int id);
    
    Task DeleteAsync(int id);
}