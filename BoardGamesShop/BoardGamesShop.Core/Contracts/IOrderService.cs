using BoardGamesShop.Core.Models.Order;

namespace BoardGamesShop.Core.Contracts;

public interface IOrderService
{
    
    /// <summary>
    /// Gets all orders
    /// </summary>
    /// <returns>An IEnumerable of OrderViewModel</returns>
    Task<IEnumerable<OrderViewModel>> GetOrdersAsync();
}