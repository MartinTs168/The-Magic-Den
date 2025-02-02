using System.Security.Claims;
using BoardGamesShop.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Controllers;

[Authorize]
public class ShoppingCartController : BaseController
{
    private readonly IShoppingService _shoppingService;
    
    public ShoppingCartController(IShoppingService shoppingService)
    {
        _shoppingService = shoppingService;
    }

    public async Task<IActionResult> Index()
    {
        var cart = await _shoppingService.GetShoppingCartByUserIdAsync(User.Id());

        if (cart == null)
        {
            int cartId = await _shoppingService.CreateShoppingCartAsync(User.Id());
            cart = await _shoppingService.GetShoppingCartByIdAsync(cartId);
        }

        return View(cart);
    }
    
}