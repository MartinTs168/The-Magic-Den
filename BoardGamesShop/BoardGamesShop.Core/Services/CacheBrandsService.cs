using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BoardGamesShop.Core.Services;

public class CacheBrandsService : ICacheBrandsService
{
    private readonly IMemoryCache _cache;
    private readonly IRepository _repository;
    private const string Key = "BrandsNames";

    public CacheBrandsService(
        IMemoryCache cache,
        IRepository repository)
    {
        _cache = cache;
        _repository = repository;
    }


    public async Task<IEnumerable<string>> GetBrandsNamesAsync()
    {
        return await _cache.GetOrCreateAsync(Key, async entry =>
        {
            var brands = await _repository.AllReadOnly<Brand>()
                .Select(b => b.Name).ToListAsync();
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return brands;            
        });
    }

    public void InvalidateCache()
    {
        _cache.Remove(Key);
    }
}