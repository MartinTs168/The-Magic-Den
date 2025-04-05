using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Controllers;

public class GameController : BaseController
{
    private readonly IGameService _gameService;
    private readonly ICacheBrandsService _cacheBrandsService;
    
    public GameController(
        IGameService gameService,
        ICacheBrandsService cacheBrandsService)
    {
        _gameService = gameService;
        _cacheBrandsService = cacheBrandsService;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> All([FromQuery] AllGamesQueryModel query)
    {
        var model = await _gameService.AllAsync(
            query.Category,
            query.SubCategory,
            query.Brand,
            query.SearchTerm,
            query.Sort,
            query.CurrentPage,
            query.GamesPerPage,
            query.SelectedBrands
        );

        query.TotalGamesCount = model.TotalGamesCount;
        query.Games = model.Games;
        query.Brands = await _cacheBrandsService.GetBrandsNamesAsync();
        return View(query);
    }
    
    [HttpGet]
    [AllowAnonymous]
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

        var model = new GameDetailsViewModel()
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            ImgUrl = game.ImgUrl,
            AgeRating = game.AgeRating,
            NumberOfPlayers = game.NumberOfPlayers,
            Price = game.Price,
            OriginalPrice = game.OriginalPrice,
            Discount = game.Discount,
            SubCategoryName = game.SubCategoryName,
            BrandName = game.BrandName,
            IsInStock = game.Quantity > 0
        };
        
        return View(model);
    }
}