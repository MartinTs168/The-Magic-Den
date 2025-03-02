namespace BoardGamesShop.Core.Contracts;

public interface ICacheCategoriesService
{

    Task<IEnumerable<string>> GetCategoriesNamesAsync();
    
    void InvalidateCache();
}