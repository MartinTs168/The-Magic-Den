namespace BoardGamesShop.Core.Contracts;

public interface ICacheBrandsService
{
    Task<IEnumerable<string>> GetBrandsNamesAsync();
    
    void InvalidateCache();
}