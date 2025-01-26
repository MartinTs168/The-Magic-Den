using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BoardGamesShop.Infrastructure.Constants.AdministratorConstants;

namespace BoardGamesShop.Controllers;

public class GameController : BaseController
{
    private readonly IGameService _gameService;
    private readonly IBrandService _brandService;
    private readonly ISubCategoryService _subCategoryService;
    
    public GameController(IGameService gameService,
        IBrandService brandService,
        ISubCategoryService subCategoryService)
    {
        _gameService = gameService;
        _brandService = brandService;
        _subCategoryService = subCategoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllGamesQueryModel query)
    {
        var model = await _gameService.AllAsync(
            query.SubCategory,
            query.SearchTerm,
            query.Sort,
            query.CurrentPage,
            query.GamesPerPage
        );

        query.TotalGamesCount = model.TotalGamesCount;
        query.Games = model.Games;
        query.SubCategories = await _gameService.AllSubCategoriesNamesAsync();
        return View(query);
    }

    [HttpGet]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Create()
    {
        var model = new GameFormModel()
        {
            Brands = await _brandService.AllAsync(),
            SubCategories = await _subCategoryService.AllAsync()
        };
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = AdminRole)]
    public async Task<IActionResult> Create(GameFormModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Brands = await _brandService.AllAsync();
            model.SubCategories = await _subCategoryService.AllAsync();
            return View(model);
        }

        await _gameService.CreateAsync(model);
        return RedirectToAction(nameof(All));
    }
}