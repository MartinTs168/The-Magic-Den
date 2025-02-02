using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Core.Services;

public class ShoppingService : IShoppingService
{
    private readonly IRepository _repository;
    
    public ShoppingService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<int> CreateShoppingCartAsync(Guid userId)
    {
        var cart = await _repository.AllReadOnly<ShoppingCart>()
            .Where(sc => sc.UserId == userId)
            .FirstOrDefaultAsync();

        if (cart == null)
        {
            cart = new ShoppingCart()
            {
                UserId = userId,
                Count = 0,
                TotalPrice = 0,
                Discount = 0,
            };
            
            await _repository.AddAsync(cart);
            await _repository.SaveChangesAsync();
        }

        return cart.Id;
    }
}