using BoardGamesShop.Core.Enumerations;
using BoardGamesShop.Core.Models.Game;

namespace BoardGamesShop.Core.Contracts;

public interface IGameService
{
    Task<GameServiceQueryModel> AllAsync(
        string? subCategory = null,
        string? searchTerm = null,
        GameSorting sorting = GameSorting.Newest,
        int currentPage = 1, int gamesPerPage = 1
        );

    Task<IEnumerable<string>> AllSubCategoriesNamesAsync();
    
    Task<IEnumerable<string>> AllBrandsNamesAsync();

    Task<int> CreateAsync(GameFormModel model);

    Task<GameFormModel?> GetGameFormModelByIdAsync(int id);
    
    Task EditAsync(GameFormModel model, int id);

    Task DeleteAsync(int id);
    
    Task<GameServiceModel?> GetGameServiceModelByIdAsync(int id);
}