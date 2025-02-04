using System.Security.Claims;
using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Cart;
using BoardGamesShop.Views.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Controllers;

public class ShoppingCartController : BaseController
{
    private readonly IShoppingService _shoppingService;
    
    public ShoppingCartController(IShoppingService shoppingService)
    {
        _shoppingService = shoppingService;
    }

    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> AddGameToCart(int gameId)
    {
        var cart = await _shoppingService.GetShoppingCartByUserIdAsync(User.Id());

        if (cart == null)
        {
            int cartId = await _shoppingService.CreateShoppingCartAsync(User.Id());
            cart = await _shoppingService.GetShoppingCartByIdAsync(cartId);
        }

        try
        {
            await _shoppingService.AddGameToCartAsync(cart.Id, gameId);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound();
        }
        
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveGameFromCart(int cartId, int gameId)
    {
        await _shoppingService.RemoveItemFromCartAsync(cartId, gameId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCartQuantity([FromBody]UpdateCartServiceModel model)
    {

        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }
        
        await _shoppingService.UpdateCartQuantityAsync(model.CartId, model.GameId, model.Quantity);

        return Json(new { success = true, message = "Cart quantity updated" });
    }

}