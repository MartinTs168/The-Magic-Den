using BoardGamesShop.Core.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace BoardGamesShop.Core.Services;

public class CachePointsService : ICachePointsService
{
    private readonly IMemoryCache _cache;
    private readonly IUserService _userService;

    public CachePointsService(IMemoryCache cache, IUserService userService)
    {
        _cache = cache;
        _userService = userService;
    }


    public async Task<int> GetCurrentValueAsync(Guid userId)
    {
        string key = $"User_{userId}_MagicPoints";
        
        return await _cache.GetOrCreateAsync(key, async entry =>
        {
            var userMagicPoints = await _userService.GetUserMagicPointsAsync(userId);
        
            if (userMagicPoints == null)
            {
                throw new InvalidOperationException("User not found");
            }

            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return (int)userMagicPoints;
        });
    }

    public void InvalidateCache(Guid userId)
    {
        string key = $"User_{userId}_MagicPoints";
        _cache.Remove(key);
    }
}