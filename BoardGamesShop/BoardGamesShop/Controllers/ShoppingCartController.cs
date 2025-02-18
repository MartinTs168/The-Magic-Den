using System.ComponentModel;
using System.Security.Claims;
using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Cart;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Controllers;

public class ShoppingCartController : BaseController
{
    private readonly IShoppingService _shoppingService;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public ShoppingCartController(
        IShoppingService shoppingService,
        UserManager<ApplicationUser> userManager)
    
    {
        _shoppingService = shoppingService;
        _userManager = userManager;
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
        int cartId;

        if (cart == null)
        {
            cartId = await _shoppingService.CreateShoppingCartAsync(User.Id());
        }
        else
        {
            cartId = cart.Id;
        }

        try
        {
            await _shoppingService.AddGameToCartAsync(cartId, gameId);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest();
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
            int newQuantity = await _shoppingService.UpdateCartQuantityAsync(cart.Id, model.GameId, model.Quantity);
            return Json(new { success = true, message = "Cart quantity updated", quantity = newQuantity });
        }
        catch (InvalidOperationException)
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetItemTotalPrice([FromQuery]int gameId)
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
        
        return Json(new
        {
            itemTotalPrice = cartItem.TotalPrice.ToString("C"),
            cartTotalPrice = cart.TotalPrice.ToString("C")
        });
    }

    [HttpPost]
    public async Task<IActionResult> ApplyDiscount([FromBody] ApplyDiscountServiceModel model)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        
        var cart = await _shoppingService.GetShoppingCartByUserIdAsync(User.Id());

        if (cart == null)
        {
            return RedirectToAction(nameof(Index));
        }

        if (cart.ShoppingCartItems.Count == 0)
        {
            return BadRequest(new { message = "Cart is empty" });
        }
        
        try
        {
            var cartTotalPrice = await _shoppingService.UpdateShoppingCartDiscountAsync(User.Id(), model.Discount);
            return Json(new
            {
                cartTotalPrice = cartTotalPrice.ToString("C")
            }); 
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var user = await _userManager.FindByIdAsync(User.Id().ToString());
        var cart = await _shoppingService.GetShoppingCartByUserIdAsync(User.Id());

        if (cart == null)
        {
            return BadRequest();
        }

        if (cart.ShoppingCartItems.Count == 0)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var model = new CheckoutViewModel()
        {
            Address = user.Address!,
            TotalPrice = cart.TotalPrice
        };
        
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _shoppingService.TransformShoppingCartToOrderAsync(User.Id(), model.Address);
            await _shoppingService.CleanShoppingCart(User.Id());
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction("Index", "Home", new { area = "" });

    }

}