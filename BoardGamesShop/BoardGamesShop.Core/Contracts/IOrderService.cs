using BoardGamesShop.Core.Models.Order;

namespace BoardGamesShop.Core.Contracts;

public interface IOrderService
{
    
    /// <summary>
    /// Gets all orders
    /// </summary>
    /// <returns>An IEnumerable of OrderViewModel</returns>
    Task<IEnumerable<OrderViewModel>> GetOrdersAsync();

    /// <summary>
    /// Gets all orders by user id
    /// </summary>
    /// <param name="userId">Property of type Guid</param>
    /// <returns>IEnumerable of OrderViewModel</returns>
    Task<IEnumerable<OrderViewModel>> GetOrdersByUserIdAsync(Guid userId);
}