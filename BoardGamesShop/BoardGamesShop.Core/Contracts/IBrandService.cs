using BoardGamesShop.Core.Models.Brand;

namespace BoardGamesShop.Core.Contracts;

public interface IBrandService
{
    Task<IEnumerable<BrandModel>> AllAsync();

    Task<int> CreateAsync(BrandModel model);

    Task EditAsync(BrandModel model, int id);
    
    Task<BrandModel?> GetByIdAsync(int id);
}