using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Order;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Core.Services;

public class OrderService : IOrderService
{

    private readonly IRepository _repository;
    
    public OrderService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<OrderViewModel>> GetOrdersAsync()
    {
        var orders = await _repository.AllReadOnly<Order>()
            .Select(o => new OrderViewModel()
            {
                OrderDate = o.CreatedAt,
                UserName = o.User.UserName,
                Quantity = o.Count,
                Discount = o.Discount,
                TotalPrice = o.TotalPrice
            })
            .ToListAsync();

        return orders;
    }
}