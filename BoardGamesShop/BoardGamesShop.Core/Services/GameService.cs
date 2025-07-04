using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Enumerations;
using BoardGamesShop.Core.Models.Brand;
using BoardGamesShop.Core.Models.Game;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Core.Services;

public class GameService : IGameService
{
    private readonly IRepository _repository;
    private readonly IBrandService  _brandService;
    private readonly ISubCategoryService _subCategoryService;
    
    public GameService(
        IRepository repository,
        IBrandService brandService, 
        ISubCategoryService subCategoryService)
    {
        _repository = repository;
        _brandService = brandService;
        _subCategoryService = subCategoryService;
    }
    
    //TODO: Add search by brand like subcategory
    public async Task<GameServiceQueryModel> AllAsync(string? category = null, string? subCategory = null, 
        string? brand = null, 
        string? searchTerm = null, 
        GameSorting sorting = GameSorting.Newest,
        int currentPage = 1, int gamesPerPage = 1,
        List<string>? selectedBrands = null)
    {
        var gamesToShow = _repository.AllReadOnly<Game>();

        if (category != null)
        {
            gamesToShow = gamesToShow
                .Where(game => game.SubCategory != null && game.SubCategory.Category.Name == category);
        }
        
        if (subCategory != null)
        {
            gamesToShow = gamesToShow
                .Where(g => g.SubCategory != null && g.SubCategory.Name == subCategory);
        }

        if (brand != null)
        {
            gamesToShow = gamesToShow
                .Where(g => g.Brand.Name == brand);
        }

        if (searchTerm != null)
        {
            string normalizedSearchTerm = searchTerm.ToLower();
            gamesToShow = gamesToShow
                .Where(g => g.Name.ToLower().Contains(normalizedSearchTerm) ||
                            g.Description.ToLower().Contains(normalizedSearchTerm) ||
                            (g.SubCategory != null && g.SubCategory.Name.ToLower().Contains(normalizedSearchTerm)));
        }

        if (selectedBrands != null && selectedBrands.Count > 0)
        {
            gamesToShow = gamesToShow
                .Where(g => selectedBrands.Contains(g.Brand.Name));
        }

        gamesToShow = sorting switch
        {
            GameSorting.PriceAscending => gamesToShow.OrderBy(g => g.Price),
            GameSorting.PriceDescending => gamesToShow.OrderByDescending(g => g.Price),
            _ => gamesToShow.OrderByDescending(g => g.Id)
        };

        var games = await gamesToShow
            .Skip((currentPage - 1) * gamesPerPage)
            .Take(gamesPerPage)
            .Select(g => new GameServiceModel()
            {
                Id = g.Id,
                Name = g.Name,
                Price = g.Price,
                ImgUrl = g.ImgUrl,
                Discount = g.Discount,
                IsInStock = g.Quantity > 0,
                OriginalPrice = g.OriginalPrice,
                SubCategoryName = g.SubCategory != null ? g.SubCategory.Name : "No Sub Category",
                BrandName = g.Brand.Name
            }).ToListAsync();
        
        int totalGames= await gamesToShow.CountAsync();

        return new GameServiceQueryModel()
        {
            TotalGamesCount = totalGames,
            Games = games
        };
    }

    public async Task<int> CreateAsync(GameFormModel model)
    {
        var gameEntity = new Game()
        {
            Name = model.Name,
            Description = model.Description,
            ImgUrl = model.ImgUrl,
            OriginalPrice = model.OriginalPrice,
            Discount = model.Discount,
            Quantity = model.Quantity,
            AgeRating = model.AgeRating,
            NumberOfPlayers = model.NumberOfPlayers,
            SubCategoryId = model.SubCategoryId,
            BrandId = model.BrandId
        };

        await _repository.AddAsync(gameEntity);
        await _repository.SaveChangesAsync();
        
        return gameEntity.Id;
    }

    public async Task<GameFormModel?> GetGameFormModelByIdAsync(int id)
    {
        var game = await _repository.GetByIdAsync<Game>(id);

        if (game == null)
        {
            return null;
        }

        return new GameFormModel()
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            ImgUrl = game.ImgUrl,
            OriginalPrice = game.OriginalPrice,
            Discount = game.Discount,
            Quantity = game.Quantity,
            AgeRating = game.AgeRating,
            NumberOfPlayers = game.NumberOfPlayers,
            SubCategoryId = game.SubCategoryId,
            BrandId = game.BrandId,
            SubCategories = await _subCategoryService.AllAsync(),
            Brands = await _brandService.AllAsync(),
        };
    }

    public async Task EditAsync(GameFormModel model, int id)
    {
        if (id > 0)
        {
            var gameEntity = await _repository.GetByIdAsync<Game>(id);
        
            if (gameEntity != null)
            {
                gameEntity.Name = model.Name;
                gameEntity.Description = model.Description;
                gameEntity.ImgUrl = model.ImgUrl;
                gameEntity.OriginalPrice = model.OriginalPrice;
                gameEntity.Discount = model.Discount;
                gameEntity.Quantity = model.Quantity;
                gameEntity.AgeRating = model.AgeRating;
                gameEntity.NumberOfPlayers = model.NumberOfPlayers;
                gameEntity.SubCategoryId = model.SubCategoryId;
                gameEntity.BrandId = model.BrandId;
        
                await _repository.SaveChangesAsync();
            }
        }
        
    }

    public async Task DeleteAsync(int id)
    {
        if (id > 0)
        {
            var gameObj = await _repository.GetByIdAsync<Game>(id);

            if (gameObj != null)
            {
                await _repository.DeleteAsync<Game>(gameObj.Id);
                await _repository.SaveChangesAsync();
            }
        }
    }

    public async Task<GameFullDetailsViewModel?> GetGameFullDetailsViewModelByIdAsync(int id)
    {
        var gameObj = await _repository.GetByIdAsync<Game>(id);

        if (gameObj == null)
        {
            return null;
        }

        return new GameFullDetailsViewModel()
        {
            Id = gameObj.Id,
            Name = gameObj.Name,
            Price = gameObj.Price,
            ImgUrl = gameObj.ImgUrl,
            Discount = gameObj.Discount,
            Quantity = gameObj.Quantity,
            OriginalPrice = gameObj.OriginalPrice,
            NumberOfPlayers = gameObj.NumberOfPlayers,
            AgeRating = gameObj.AgeRating,
            Description = gameObj.Description,
            SubCategoryName = gameObj.SubCategory != null ? gameObj.SubCategory.Name : "No Sub Category",
            BrandName = gameObj.Brand.Name
        };

    }

    public async Task<IEnumerable<GameServiceModel>> GetFiveNewestGamesAsync()
    {
        var games = _repository.All<Game>()
            .OrderByDescending(game => game.Id)
            .Take(5);

        return await games.Select(game => new GameServiceModel()
        {
            Id = game.Id,
            Name = game.Name,
            BrandName = game.Brand.Name,
            SubCategoryName = game.SubCategory!.Name,
            Price = game.Price,
            OriginalPrice = game.OriginalPrice,
            Discount = game.Discount,
            ImgUrl = game.ImgUrl,
            IsInStock = game.Quantity > 0
        }).ToListAsync();
    }
}