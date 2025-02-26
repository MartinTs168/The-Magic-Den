using BoardGamesShop.Models;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using BoardGamesShop.Core.Contracts;

namespace BoardGamesShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameService _gameService;

        public HomeController(
            ILogger<HomeController> logger,
            IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public async  Task<IActionResult> Index()
        {

            var model = await _gameService.GetFiveNewestGamesAsync();
            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
