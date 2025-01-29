using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Statistic;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Areas.Admin.Controllers;

public class StatisticController : AdminBaseController
{

    private readonly IStatisticService _statisticService;
    
    public StatisticController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        StatisticViewModel model = new StatisticViewModel();
        model.CountClients = await _statisticService.CountClientsAsync();   
        model.CountGames = await _statisticService.CountGamesAsync();
        
        return View(model);
    }
}