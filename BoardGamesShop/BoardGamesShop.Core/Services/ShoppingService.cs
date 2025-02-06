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
    private readonly UserManager<ApplicationUser> _userManager;
    
    public ShoppingService(IRepository repository,
        UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
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

        var cartItem = new ShoppingCartItem()
        {
            GameId = gameId,
            ShoppingCartId = cartId,
            Quantity = 1
        };
        

        await _repository.AddAsync(cartItem);
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

    public async Task UpdateCartQuantityAsync(int cartId, int gameId, int quantity)
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
        
        cartItem.Quantity = quantity;
        cart.UpdateCart();
        
        await _repository.SaveChangesAsync();
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
}