using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Game;
using static BoardGamesShop.Core.Constants.MessageConstants;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Areas.Admin.Controllers;

public class GameController : AdminBaseController
{
    private readonly IGameService _gameService;
    private readonly IBrandService _brandService;
    private readonly ISubCategoryService _subCategoryService;
    private readonly ICacheBrandsService _cacheBrandsService;
    private readonly ICacheSubCategoriesService _cacheSubCategoriesService;
    private readonly ICacheCategoriesService _cacheCategoriesService;
    
    public GameController(IGameService gameService,
        IBrandService brandService,
        ISubCategoryService subCategoryService,
        ICacheBrandsService cacheBrandsService,
        ICacheSubCategoriesService cacheSubCategoriesService,
        ICacheCategoriesService cacheCategoriesService)
    {
        _gameService = gameService;
        _brandService = brandService;
        _subCategoryService = subCategoryService;
        _cacheBrandsService = cacheBrandsService;
        _cacheSubCategoriesService = cacheSubCategoriesService;
        _cacheCategoriesService = cacheCategoriesService;
    }
    
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllGamesQueryModel query)
    {
        var model = await _gameService.AllAsync(
            query.Category,
            query.SubCategory,
            query.Brand,
            query.SearchTerm,
            query.Sort,
            query.CurrentPage,
            query.GamesPerPage
        );

        query.TotalGamesCount = model.TotalGamesCount;
        query.Games = model.Games;
        
        var subCategories = await _cacheSubCategoriesService.GetSubCategoriesNamesAsync();
        IEnumerable<string> subCategoriesAsEnumerable =
            subCategories.Values.SelectMany(subCategoryList => subCategoryList.Select(sc => sc));

        query.SubCategories = subCategoriesAsEnumerable;
        query.Brands = await _cacheBrandsService.GetBrandsNamesAsync();
        query.Categories = await _cacheCategoriesService.GetCategoriesNamesAsync();
        return View(query);
    }

    [HttpGet]
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
    public async Task<IActionResult> Create(GameFormModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Brands = await _brandService.AllAsync();
            model.SubCategories = await _subCategoryService.AllAsync();
            return View(model);
        }

        await _gameService.CreateAsync(model);
        TempData[UserMessageSuccess] = "Game created successfully";
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var gameForm = await _gameService.GetGameFormModelByIdAsync(id);
        
        if (gameForm == null)
        {
            return NotFound();
        }
        
        return View(gameForm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GameFormModel model, int id)
    {
        if (id <= 0 ||!ModelState.IsValid)
        {
            model.Brands = await _brandService.AllAsync();
            model.SubCategories = await _subCategoryService.AllAsync();
            return View(model);
        }

        await _gameService.EditAsync(model, id);
        TempData[UserMessageSuccess] = "Game updated successfully";
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var game = await _gameService.GetGameFormModelByIdAsync(id);
        
        if (game == null)
        {
            return NotFound();
        }
        
        game.SubCategories = await _subCategoryService.AllAsync();
        game.Brands = await _brandService.AllAsync();
        return View(game);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(GameFormModel model)
    {
        await _gameService.DeleteAsync(model.Id);
        TempData[UserMessageSuccess] = "Game deleted successfully";
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var game = await _gameService.GetGameFullDetailsViewModelByIdAsync(id);
        
        if (game == null)
        {
            return NotFound();
        }
        
        return View(game);
    }
}