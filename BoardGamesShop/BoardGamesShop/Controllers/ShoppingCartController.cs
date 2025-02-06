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
    public async Task<IActionResult> RemoveGameFromCart(int gameId)
    {

        var cart = await _shoppingService.GetShoppingCartByUserIdAsync(User.Id());

        if (cart == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        try
        {
            await _shoppingService.RemoveItemFromCartAsync(cart.Id, gameId);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCartQuantity([FromBody]UpdateCartServiceModel model)
    {
        var cart = await _shoppingService.GetShoppingCartByUserIdAsync(User.Id());

        if (cart == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        if (ModelState.IsValid == false)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _shoppingService.UpdateCartQuantityAsync(cart.Id, model.GameId, model.Quantity);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest();
        }

        return Json(new { success = true, message = "Cart quantity updated" });
    }

    [HttpGet]
    public async Task<IActionResult> GetItemTotalPrice(int gameId)
    {
        var cart = await _shoppingService.GetShoppingCartByUserIdAsync(User.Id());

        if (cart == null)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var cartItem = await _shoppingService.GetShoppingCartItemsAsync(cart.Id, gameId);
        
        if (cartItem == null)
        {
            return NotFound();
        }
        
        return Json(new { success = true, totalPrice = cartItem.TotalPrice });
    }

}