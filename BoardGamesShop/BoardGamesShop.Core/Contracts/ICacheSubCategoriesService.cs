using System.ComponentModel;

namespace BoardGamesShop.Core.Contracts;

public interface ICacheSubCategoriesService
{
    Task<Dictionary<string, List<string>>> GetSubCategoriesNamesAsync();
    
    void InvalidateCache();
}