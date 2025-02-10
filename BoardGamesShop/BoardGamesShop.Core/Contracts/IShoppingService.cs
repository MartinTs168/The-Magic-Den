using BoardGamesShop.Core.Models.Cart;

namespace BoardGamesShop.Core.Contracts;

public interface IShoppingService
{
    Task<int> CreateShoppingCartAsync(Guid userId);
    
    Task<ShoppingCartViewModel?> GetShoppingCartByUserIdAsync(Guid userId);
    
    Task<ShoppingCartViewModel?> GetShoppingCartByIdAsync(int id);

    Task AddGameToCartAsync(int cartId, int gameId);

    Task RemoveItemFromCartAsync(int cartId, int gameId);
    
    /// <summary>
    /// Updates the value of the item in the cart and the whole cart
    /// </summary>
    /// <param name="cartId"></param>
    /// <param name="gameId"></param>
    /// <param name="quantity"></param>
    /// <returns>The quantity of the updated cart item</returns>
    Task<int> UpdateCartQuantityAsync(int cartId, int gameId, int quantity);

    Task<ShoppingCartItemServiceModel?> GetShoppingCartItemsAsync(int cartId, int gameId);
}