using BoardGamesShop.Core.Models.Cart;

namespace BoardGamesShop.Core.Contracts;

public interface IShoppingService
{
    Task<int> CreateShoppingCartAsync(Guid userId);
    
    Task<ShoppingCartViewModel?> GetShoppingCartByUserIdAsync(Guid userId);
    
    Task<ShoppingCartViewModel?> GetShoppingCartByIdAsync(int id);

    Task AddGameToCartAsync(int cartId, int gameId);

    Task RemoveItemFromCartAsync(int cartId, int gameId);
    
    Task UpdateCartQuantityAsync(int cartId, int gameId, int quantity);

    Task<ShoppingCartItemServiceModel?> GetShoppingCartItemsAsync(int cartId, int gameId);
}