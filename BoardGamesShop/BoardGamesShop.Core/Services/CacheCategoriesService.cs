using BoardGamesShop.Core.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace BoardGamesShop.Core.Services;

public class CacheCategoriesService : ICacheCategoriesService
{
    private readonly IMemoryCache _cache;
    private readonly ICategoryService _categoryService;
    private const string Key = "CategoriesNames";

    public CacheCategoriesService(
        IMemoryCache cache,
        ICategoryService categoryService)
    {
        _cache = cache;
        _categoryService = categoryService;
    }


    public async Task<IEnumerable<string>> GetCategoriesNamesAsync()
    {
        return await _cache.GetOrCreateAsync(Key, async entry =>
        {
            var categories = await _categoryService.AllCategoriesNamesAsync();
             entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return categories;            
        });
    }

    public void InvalidateCache()
    {
        _cache.Remove(Key);
    }
}