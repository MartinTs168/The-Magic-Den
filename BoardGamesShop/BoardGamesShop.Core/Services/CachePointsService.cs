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

        var userMagicPoints = await _userService.GetUserMagicPointsAsync(userId);
        
        if (userMagicPoints == null)
        {
            throw new InvalidOperationException("User not found");
        }

        return await _cache.GetOrCreateAsync("MagicPoints", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return (int)userMagicPoints;
        });
    }

    public void InvalidateCache()
    {
        _cache.Remove("MagicPoints");
    }
}