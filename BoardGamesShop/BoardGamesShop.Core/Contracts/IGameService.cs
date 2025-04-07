using BoardGamesShop.Core.Enumerations;
using BoardGamesShop.Core.Models.Game;

namespace BoardGamesShop.Core.Contracts;

public interface IGameService
{
    Task<GameServiceQueryModel> AllAsync(
        string? category = null,
        string? subCategory = null,
        string? brand = null,
        string? searchTerm = null,
        GameSorting sorting = GameSorting.Newest,
        int currentPage = 1, 
        int gamesPerPage = 1,
        List<string>? selectedBrands = null
        );
    
    Task<int> CreateAsync(GameFormModel model);

    Task<GameFormModel?> GetGameFormModelByIdAsync(int id);
    
    Task EditAsync(GameFormModel model, int id);

    Task DeleteAsync(int id);
    
    Task<GameFullDetailsViewModel?> GetGameFullDetailsViewModelByIdAsync(int id);

    Task<IEnumerable<GameServiceModel>> GetFiveNewestGamesAsync();
}