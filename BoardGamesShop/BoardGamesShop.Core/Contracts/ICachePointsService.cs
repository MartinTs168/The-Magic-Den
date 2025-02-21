namespace BoardGamesShop.Core.Contracts;

public interface ICachePointsService
{
    Task<int> GetCurrentValueAsync(Guid userId);

    Task InvalidateCacheAsync();
}