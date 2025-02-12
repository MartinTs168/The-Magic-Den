using BoardGamesShop.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Areas.Admin.Controllers;

public class OrderController : AdminBaseController
{
    private readonly IOrderService _orderService;
    
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrdersAsync();
        return View(orders);
    }
}