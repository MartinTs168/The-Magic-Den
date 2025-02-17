using BoardGamesShop.Core.Enumerations;
using BoardGamesShop.Core.Models.Cart;
using BoardGamesShop.Infrastructure.Data.Entities;

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

    /// <summary>
    /// Transforms a ShoppingCartItem to OrderItem
    /// and saves it to tbe database
    /// </summary>
    /// <param name="item"> ShoppingCartItem</param>
    /// <param name="orderId"> int id of Order</param>
    /// <returns>The object added to the db</returns>
    Task<OrderItem> TransformShoppingCartItemToOrderItemAsync(ShoppingCartItem item, int orderId);

    /// <summary>
    /// Gets a ShoppingCart object and turns it into Order
    /// </summary>
    /// <param name="userId">Guid id of User</param>
    /// <param name="address">string address to add to order</param>
    Task TransformShoppingCartToOrderAsync(Guid userId, string address);

    /// <summary>
    /// Deletes the shopping cart and its items
    /// </summary>
    /// <param name="userId">Guid id of User</param>
    /// <returns></returns>
    Task CleanShoppingCart(Guid userId);

    /// <summary>
    /// Updates the discount of the shopping cart
    /// </summary>
    /// <param name="userId">Guid id of User</param>
    /// <param name="discount">ShoppingCartDiscount enum default ZeroPercent</param>
    /// <returns></returns>
    Task UpdateShoppingCartDiscountAsync(Guid userId, ShoppingCartDiscount discount = ShoppingCartDiscount.ZeroPercent);
}