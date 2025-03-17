using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Controllers;

public class ContactUsController : BaseController
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }
}