using BoardGamesShop.Core.Models.Brand;

namespace BoardGamesShop.Core.Contracts;

public interface IBrandService
{
    Task<IEnumerable<BrandModel>> AllAsync();
}