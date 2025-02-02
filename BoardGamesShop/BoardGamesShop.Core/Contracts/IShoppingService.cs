namespace BoardGamesShop.Core.Contracts;

public interface IShoppingService
{
    Task<int> CreateShoppingCartAsync(Guid userId);
}