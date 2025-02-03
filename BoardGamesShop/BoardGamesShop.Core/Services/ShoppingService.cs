using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Cart;
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

    public async Task<ShoppingCartViewModel?> GetShoppingCartByUserIdAsync(Guid userId)
    {
        var cart = await _repository.All<ShoppingCart>()
            .Where(sc => sc.UserId == userId)
            .FirstOrDefaultAsync();
        
        if (cart == null)
        {
            return null;
        }
        
        var items = cart.ShoppingCartItems
            .Select(sci => new ShoppingCartItemServiceModel()
            {
                Name = sci.Game.Name,
                ImgUrl = sci.Game.ImgUrl,
                Quantity = sci.Quantity,
                TotalPrice = sci.TotalPrice,
            }).ToList();

        return new ShoppingCartViewModel()
        {
            Id = cart.Id,
            TotalPrice = cart.TotalPrice,
            Count = cart.Count,
            Discount = cart.Discount,
            ShoppingCartItems = items
        };
    }

    public async Task<ShoppingCartViewModel?> GetShoppingCartByIdAsync(int id)
    {
        var cart = await _repository.GetByIdAsync<ShoppingCart>(id);
        
        if (cart == null)
        {
            return null;
        } 
        
        var items = cart.ShoppingCartItems
            .Select(sci => new ShoppingCartItemServiceModel()
            {
                Name = sci.Game.Name,
                ImgUrl = sci.Game.ImgUrl,
                Quantity = sci.Quantity,
                TotalPrice = sci.TotalPrice,
            }).ToList();
        
        return new ShoppingCartViewModel()
        {
            Id = cart.Id,
            TotalPrice = cart.TotalPrice,
            Count = cart.Count,
            Discount = cart.Discount,
            ShoppingCartItems = items
        };
    }

    public async Task AddGameToCartAsync(int cartId, int gameId)
    {
        var cart = await _repository.GetByIdAsync<ShoppingCart>(cartId);
        
        if (cart == null)
        {
            throw new InvalidOperationException("Cart not found");
        }
        
        var game = await _repository.GetByIdAsync<Game>(gameId);
        
        if (game == null)
        {
            throw new InvalidOperationException("Game not found");
        }

        var cartItem = new ShoppingCartItem()
        {
            GameId = gameId,
            ShoppingCartId = cartId,
            Quantity = 1
        };

        await _repository.AddAsync(cartItem);
        await _repository.SaveChangesAsync();
    }
}