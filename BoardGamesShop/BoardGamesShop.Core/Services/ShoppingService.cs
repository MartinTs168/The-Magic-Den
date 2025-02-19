using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Enumerations;
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
    private const int FivePercentDiscountCost = 5000;
    private const int FifteenPercentDiscountCost = 10000;
    private const int FiftyPercentDiscountCost = 50000;
    
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


        ShoppingCartDiscount discount = cart.Discount switch
        {
            5 => ShoppingCartDiscount.FivePercent,
            15 => ShoppingCartDiscount.FifteenPercent,
            50 => ShoppingCartDiscount.FiftyPercent,
            _ => ShoppingCartDiscount.ZeroPercent
        };
        
        return new ShoppingCartViewModel()
        {
            Id = cart.Id,
            TotalPrice = cart.TotalPrice,
            Count = cart.Count,
            Discount = discount,
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
        
        ShoppingCartDiscount discount = cart.Discount switch
        {
            5 => ShoppingCartDiscount.FivePercent,
            15 => ShoppingCartDiscount.FifteenPercent,
            50 => ShoppingCartDiscount.FiftyPercent,
            _ => ShoppingCartDiscount.ZeroPercent
        };
        
        return new ShoppingCartViewModel()
        {
            Id = cart.Id,
            TotalPrice = cart.TotalPrice,
            Count = cart.Count,
            Discount = discount,
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
        
        var user = await _repository.GetByIdAsync<ApplicationUser>(userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }
        
        int userMagicPoints = user.MagicPoints;
        
        
        switch (cart.Discount)
        {
            case 5:
                if (userMagicPoints < FivePercentDiscountCost)
                {
                    throw new ArgumentException("Insufficient magic points");
                }

                break;
            case 15:
                if (userMagicPoints < FifteenPercentDiscountCost)
                {
                    throw new ArgumentException("Insufficient magic points");
                }

                break;
            case 50:
                if (userMagicPoints < FiftyPercentDiscountCost)
                {
                    throw new ArgumentException("Insufficient magic points");
                }

                break;
        }
        
        var shoppingCartItems = await _repository.All<ShoppingCartItem>()
            .Where(sci => sci.ShoppingCartId == cart.Id)
            .ToListAsync();

        if (shoppingCartItems.Count == 0)
        {
            throw new InvalidOperationException("Cart is empty");
        }
        
        foreach (var item in shoppingCartItems)
        {
            if (item.Quantity > item.Game.Quantity || item.Quantity <= 0)
            {
                throw new InvalidOperationException("Quantity of item cannot " +
                                                    "exceed the available quantity or be 0 or less.");
            }
        }
        
        var order = new Order()
        {
            UserId = userId,
            Address = address,
            TotalPrice = cart.TotalPrice,
            CreatedAt = DateTime.Now,
            Count = cart.Count,
            Discount = cart.Discount
        };

        user.MagicPoints += decimal.ToInt32(cart.TotalPrice * 100);
        
        await _repository.AddAsync(order);
        await _repository.SaveChangesAsync();
        
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

    public async Task<decimal> UpdateShoppingCartDiscountAsync(Guid userId, ShoppingCartDiscount discount)
    {
        var cart = await _repository.All<ShoppingCart>()
           .Where(sc => sc.UserId == userId)
           .FirstOrDefaultAsync();

        if (cart == null)
        {
            throw new InvalidOperationException("Cart not found");
        }
        
        switch (discount)
        {
            case ShoppingCartDiscount.ZeroPercent:
                cart.Discount = 0;
                break;
            case ShoppingCartDiscount.FivePercent:
                cart.Discount = 5;
                break;
            case ShoppingCartDiscount.FifteenPercent:
                cart.Discount = 15;
                break;
            case ShoppingCartDiscount.FiftyPercent:
                cart.Discount = 50;
                break;
            default: throw new ArgumentException("Invalid value");
                
        }
        
        cart.UpdateCart();
        await _repository.SaveChangesAsync();
        
        return cart.TotalPrice;
    }
}