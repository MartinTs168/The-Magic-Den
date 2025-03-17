using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Controllers;

public class ContactUsController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}