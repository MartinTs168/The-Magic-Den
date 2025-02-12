using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Cart;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

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
                GameId = sci.GameId
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
                GameId = sci.GameId
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

        if (game.Quantity <= 0)
        {
            throw new InvalidOperationException("Game out of stock");
        }
        
        ShoppingCartItem? cartItem = await _repository.GetByIdAsync<ShoppingCartItem>(cartId, gameId);

        if (cartItem == null)
        {

            cartItem = new ShoppingCartItem()
            {
                GameId = gameId,
                ShoppingCartId = cartId,
                Quantity = 1
            };


            await _repository.AddAsync(cartItem);
        }
        else
        {
            cartItem.Quantity++; 
        }

        cart.UpdateCart();
        await _repository.SaveChangesAsync();
    }

    public async Task RemoveItemFromCartAsync(int cartId, int gameId)
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

        var cartItem = await _repository.GetByIdAsync<ShoppingCartItem>(cartId, gameId);
        
        if (cartItem == null)
        {
            throw new InvalidOperationException("Item not found in the cart");
        }

        cart.ShoppingCartItems.Remove(cartItem);
        
        cart.UpdateCart();
        
        await _repository.DeleteAsync<ShoppingCartItem>(cartId, gameId);
        await _repository.SaveChangesAsync();
    }

    public async Task<int> UpdateCartQuantityAsync(int cartId, int gameId, int quantity)
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

        var cartItem = await _repository.GetByIdAsync<ShoppingCartItem>(cartId, gameId);
        
        if (cartItem == null)
        {
            throw new InvalidOperationException("Item not found in the cart");
        }

        cartItem.Quantity = quantity > game.Quantity ? game.Quantity : quantity;
        
        cart.UpdateCart();
        
        await _repository.SaveChangesAsync();

        return cartItem.Quantity;
    }

    public async Task<ShoppingCartItemServiceModel?> GetShoppingCartItemsAsync(int cartId, int gameId)
    {
        var cartItem = await  _repository.GetByIdAsync<ShoppingCartItem>(cartId, gameId);

        if (cartItem == null)
        {
            return null;
        }

        return new ShoppingCartItemServiceModel()
        {
            GameId = gameId,
            Name = cartItem.Game.Name,
            ImgUrl = cartItem.Game.ImgUrl,
            Quantity = cartItem.Quantity,
            TotalPrice = cartItem.TotalPrice
        };
    }

    public async Task<OrderItem> TransformShoppingCartItemToOrderItemAsync(ShoppingCartItem item, int orderId)
    {
        var orderItem = new OrderItem()
        {
            GameId = item.GameId,
            Quantity = item.Quantity,
            OrderId = orderId,
        };

        await _repository.AddAsync(orderItem);
        item.Game.Quantity -= item.Quantity;
        
        return orderItem;
    }

    public async Task TransformShoppingCartToOrderAsync(Guid userId, string address)
    {
        var cart = await _repository.AllReadOnly<ShoppingCart>()
            .Where(sc => sc.UserId == userId)
            .FirstOrDefaultAsync();
        
        if (cart == null)
        {
            throw new InvalidOperationException("Cart not found");
        }

        var order = new Order()
        {
            UserId = userId,
            Address = address,
            TotalPrice = cart.TotalPrice,
            CreatedAt = DateTime.Now,
            Count = cart.Count
        };
        
        await _repository.AddAsync(order);
        await _repository.SaveChangesAsync();

        var shoppingCartItems = await _repository.All<ShoppingCartItem>()
            .Where(sci => sci.ShoppingCartId == cart.Id)
            .ToListAsync();

        foreach (var item in shoppingCartItems)
        {
            if (item.Quantity > item.Game.Quantity)
            {
                throw new InvalidOperationException("Quantity of item cannot exceed the available quantity");
            }
        }
        
        
        foreach (var item in shoppingCartItems)
        {
            await TransformShoppingCartItemToOrderItemAsync(item, order.Id);
        }

        await _repository.SaveChangesAsync();
    }

    public async Task CleanShoppingCart(Guid userId)
    {
        var cart = await _repository.AllReadOnly<ShoppingCart>()
            .Where(sc => sc.UserId == userId)
            .FirstOrDefaultAsync();
        
        if (cart == null)
        {
            throw new InvalidOperationException("Cart not found");
        }
        
        var shoppingCartItems = await _repository.All<ShoppingCartItem>()
            .Where(sci => sci.ShoppingCartId == cart.Id)
            .ToListAsync();

        foreach (var item in shoppingCartItems)
        {
            await _repository.DeleteAsync<ShoppingCartItem>(item.ShoppingCartId, item.GameId);
        }

        await _repository.DeleteAsync<ShoppingCart>(cart.Id);
        
        await _repository.SaveChangesAsync();
    }
}