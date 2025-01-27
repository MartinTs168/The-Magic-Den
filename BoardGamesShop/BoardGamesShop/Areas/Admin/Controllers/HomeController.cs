using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Areas.Admin.Controllers;

public class HomeController : AdminBaseController
{
    // GET
    public IActionResult Dashboard()
    {
        return View();
    }
}