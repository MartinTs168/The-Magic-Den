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
        return await _repository.AllReadOnly<Order>()
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderViewModel()
            {
                OrderDate = o.CreatedAt,
                UserName = o.User.UserName,
                Quantity = o.Count,
                Discount = o.Discount,
                TotalPrice = o.TotalPrice
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderViewModel>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await _repository.AllReadOnly<Order>()
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderViewModel()
            {
                OrderDate = o.CreatedAt,
                UserName = o.User.UserName,
                Quantity = o.Count,
                Discount = o.Discount,
                TotalPrice = o.TotalPrice
            })
            .ToListAsync();
    }
}