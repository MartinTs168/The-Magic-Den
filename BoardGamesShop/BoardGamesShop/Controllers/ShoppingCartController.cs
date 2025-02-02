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
    
}