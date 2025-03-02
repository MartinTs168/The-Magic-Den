using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BoardGamesShop.Core.Services;

public class CacheCategoriesService : ICacheCategoriesService
{
    private readonly IMemoryCache _cache;
    private readonly IRepository _repository;
    private const string Key = "CategoriesNames";

    public CacheCategoriesService(
        IMemoryCache cache,
        IRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }


    public async Task<IEnumerable<string>> GetCategoriesNamesAsync()
    {
        return await _cache.GetOrCreateAsync(Key, async entry =>
        {
            var categories = await _repository.AllReadOnly<Category>()
                .Select(c => c.Name).ToListAsync();
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return categories;            
        });
    }

    public void InvalidateCache()
    {
        _cache.Remove(Key);
    }
}