using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BoardGamesShop.Infrastructure.Constants.AdministratorConstants;

namespace BoardGamesShop.Controllers;

public class GameController : BaseController
{
    private readonly IGameService _gameService;
    
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> All([FromQuery] AllGamesQueryModel query)
    {
        var model = await _gameService.AllAsync(
            query.SubCategory,
            query.Brand,
            query.SearchTerm,
            query.Sort,
            query.CurrentPage,
            query.GamesPerPage
        );

        query.TotalGamesCount = model.TotalGamesCount;
        query.Games = model.Games;
        query.SubCategories = await _gameService.AllSubCategoriesNamesAsync();
        query.Brands = await _gameService.AllBrandsNamesAsync();
        return View(query);
    }
}