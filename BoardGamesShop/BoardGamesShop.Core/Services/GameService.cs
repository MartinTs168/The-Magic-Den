using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Enumerations;
using BoardGamesShop.Core.Models.Game;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Core.Services;

public class GameService : IGameService
{
    private readonly IRepository _repository;
    
    public GameService(IRepository repository)
    {
        _repository = repository;
    }
    
    //TODO: Add search by brand like subcategory
    public async Task<GameServiceQueryModel> AllAsync(string? subCategory = null, string? searchTerm = null, 
        GameSorting sorting = GameSorting.Newest,
        int currentPage = 1, int gamesPerPage = 1)
    {
        var gamesToShow = _repository.AllReadOnly<Game>();

        if (subCategory != null)
        {
            gamesToShow = gamesToShow
                .Where(g => g.SubCategory != null && g.SubCategory.Name == subCategory);
        }

        if (searchTerm != null)
        {
            string normalizedSearchTerm = searchTerm.ToLower();
            gamesToShow = gamesToShow
                .Where(g => g.Name.ToLower().Contains(normalizedSearchTerm) ||
                            g.Description.ToLower().Contains(normalizedSearchTerm) ||
                            (g.SubCategory != null && g.SubCategory.Name.ToLower().Contains(normalizedSearchTerm)));
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
                AgeRating = g.AgeRating,
                NumberOfPlayers = g.NumberOfPlayers,
                Description = g.Description,
                Discount = g.Discount,
                Quantity = g.Quantity,
                OriginalPrice = g.OriginalPrice,
            }).ToListAsync();
        
        int totalGames= await gamesToShow.CountAsync();

        return new GameServiceQueryModel()
        {
            TotalGamesCount = totalGames,
            Games = games
        };
    }

    public async Task<IEnumerable<string>> AllSubCategoriesNamesAsync()
    {
        return await _repository.AllReadOnly<SubCategory>()
            .Select(sc => sc.Name)
            .ToListAsync();
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

    public async Task<GameServiceModel?> GetByIdAsync(int id)
    {
        if (id < 0) return null;

        var game = await _repository.GetByIdAsync<Game>(id);

        if (game == null) return null;

        return new GameServiceModel()
        {
            Id = game.Id,
            Name = game.Name,
            Price = game.Price,
            ImgUrl = game.ImgUrl,
            AgeRating = game.AgeRating,
            NumberOfPlayers = game.NumberOfPlayers,
            Description = game.Description,
            Discount = game.Discount,
            Quantity = game.Quantity,
            OriginalPrice = game.OriginalPrice,
            SubCategory = game.SubCategory?.Name ?? null,
            BrandName = game.Brand.Name
        };

    }
}