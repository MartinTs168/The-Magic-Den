using BoardGamesShop.Core.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace BoardGamesShop.Core.Services;

public class CachePointsService : ICachePointsService
{
    private readonly IMemoryCache _cahce;
    
    
    public Task<int> GetCurrentValueAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task InvalidateCacheAsync()
    {
        throw new NotImplementedException();
    }
}