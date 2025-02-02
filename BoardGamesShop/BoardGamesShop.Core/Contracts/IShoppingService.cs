using BoardGamesShop.Core.Models.Cart;

namespace BoardGamesShop.Core.Contracts;

public interface IShoppingService
{
    Task<int> CreateShoppingCartAsync(Guid userId);
    
    Task<ShoppingCartViewModel?> GetShoppingCartByUserIdAsync(Guid userId);
}