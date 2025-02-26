using System.Collections;
using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace BoardGamesShop.Core.Services;

public class CacheSubCategoriesService : ICacheSubCategoriesService
{
    private readonly IMemoryCache _cache;
    private readonly IRepository _repository;
    private const string Key = "SubCategoriesNames";


    public CacheSubCategoriesService(
        IMemoryCache cache, 
        IRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }


    public async Task<Dictionary<string, List<string>>> GetSubCategoriesNames()
    {
        
        return await _cache.GetOrCreateAsync(Key, async entry =>
        {
            Dictionary<string, List<string>> subCategoriesNames = new Dictionary<string, List<string>>();

            foreach (var subCategory in _repository.All<SubCategory>())
            {
                if (!subCategoriesNames.ContainsKey(subCategory.Category.Name))
                {
                    subCategoriesNames.Add(subCategory.Category.Name, new List<string>());
                }
                
                subCategoriesNames[subCategory.Category.Name].Add(subCategory.Name);
            }
            
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return subCategoriesNames;
        });
    }

    public void InvalidateCache()
    {
        _cache.Remove(Key);
    }
}