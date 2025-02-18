using System.Security.Claims;
using BoardGamesShop.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Controllers;

public class OrderController : BaseController
{

    private readonly IOrderService _orderService;
    
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet]
    public async Task<IActionResult> MyOrders()
    {
        return View(await _orderService.GetOrdersByUserIdAsync(User.Id()));
    }
}