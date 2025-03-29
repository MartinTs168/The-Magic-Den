using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Services;
using BoardGamesShop.Infrastructure.Data;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Tests;

public class ShoppingServiceIntegrationTests
{
    private ApplicationDbContext _context = null!;
    private SqliteConnection _connection = null!;
    private IRepository _repository = null!;
    private IShoppingService _service = null!;
    private ICachePointsService _cachePointsService = null!;
    private IMemoryCache _cache = null!;
    private IUserService _userService = null!;

    private Guid _testuser1Id = Guid.NewGuid();
    private Guid _testuser2Id = Guid.NewGuid();
    private Guid _testuser3Id = Guid.NewGuid();
    private Guid _testuser4Id = Guid.NewGuid();

    [SetUp]
    public async Task Setup()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();
        
        
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();

        await SeedDatabase();

        _repository = new Repository(_context, false);

        var mockUserService = new Mock<IUserService>();
        _userService = mockUserService.Object;
        
        _cache = new MemoryCache(new MemoryCacheOptions());
        _cachePointsService = new CachePointsService(_cache, _userService);
        _service = new ShoppingService(_repository, _cachePointsService);
    }

    private async Task SeedDatabase()
    {
        var users = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                Id = _testuser1Id,
                UserName = "testuser1",
                FirstName = "Test",
                LastName = "User",
                Email = "test@example.com",
                MagicPoints = 1000
            },
            new ApplicationUser()
            {
                Id = _testuser2Id,
                UserName = "testuser2",
                FirstName = "Test",
                LastName = "User",
                Email = "test2@example.com",
                MagicPoints = 5000
            },
            new ApplicationUser()
            {
                Id = _testuser3Id,
                UserName = "testuser3",
                FirstName = "Test",
                LastName = "User",
                Email = "test3@example.com",
                MagicPoints = 10000
            },
            new ApplicationUser()
            {
                Id = _testuser4Id,
                UserName = "testuser4",
                FirstName = "Test",
                LastName = "User",
                Email = "test4@example.com",
                MagicPoints = 50000
            }
        };

        var games = new List<Game>()
        {
            new Game
            {
                Id = 1, 
                Name = "Game 1", 
                OriginalPrice = 50, 
                Quantity = 100, 
                Discount = 0, 
                ImgUrl = "game1.jpg",
                AgeRating = "test",
                Description = "testing",
                NumberOfPlayers = "1",
                BrandId = 1
            },
            new Game
            {
                Id = 2, 
                Name = "Game 2",
                OriginalPrice = 40,
                Quantity = 100,
                Discount = 0,
                ImgUrl = "game1.jpg",
                AgeRating = "test",
                Description = "testing",
                NumberOfPlayers = "1",
                BrandId = 1
                
            },
            new Game 
            { 
                Id = 3,
                Name = "Game 3",
                OriginalPrice = 30,
                Quantity = 100,
                Discount = 0,
                ImgUrl = "game1.jpg",
                AgeRating = "test",
                Description = "testing",
                NumberOfPlayers = "1",
                BrandId = 1
                
            },
        };
        
        var carts = new List<ShoppingCart>
        {
            new ShoppingCart
            {
                Id = 1, 
                UserId = _testuser1Id,
            },
            
            new ShoppingCart
            {
                Id = 2, 
                UserId = _testuser2Id,
                Discount = 5
            },
            
            new ShoppingCart
            {
                Id = 3, 
                UserId = _testuser3Id,
                Discount = 15
            },
            
            new ShoppingCart
            {
                Id = 4, 
                UserId = _testuser4Id,
                Discount = 50
            }
        };
        
        var ShooppingCartItems = new List<ShoppingCartItem>()
        {
            new ShoppingCartItem { ShoppingCartId = 1, GameId = 1, Quantity = 2},
            new ShoppingCartItem {ShoppingCartId = 1, GameId = 2, Quantity = 1},
            new ShoppingCartItem { ShoppingCartId = 2, GameId = 2, Quantity = 1},
            new ShoppingCartItem { ShoppingCartId = 3, GameId = 3, Quantity = 3},
            new ShoppingCartItem {ShoppingCartId = 4, GameId = 1, Quantity = 1}
        };
        
        _context.Users.AddRange(users);
        _context.Games.AddRange(games);
        _context.ShoppingCarts.AddRange(carts);
        _context.ShoppingCartItems.AddRange(ShooppingCartItems);
        await _context.SaveChangesAsync();
    }

    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
        if (_connection != null)
        {
            _connection.Close();
            _connection.Dispose();
        }
        _cache.Dispose();
    }

    [Test]
    public async Task TransformShoppingCartToOrderAsync_ShouldCreateOrderWithCorrectParameters_AndUpdateValueOfGamesQuantityAndUserMagicPoints()
    {
        var shoppingCart = await _context.ShoppingCarts
            .Where(sh => sh.UserId == _testuser1Id).FirstOrDefaultAsync();

        var game1 = await _repository.GetByIdAsync<Game>(1);
        var game2 = await _repository.GetByIdAsync<Game>(2);

        var user = await _repository.GetByIdAsync<ApplicationUser>(_testuser1Id);
        
        shoppingCart!.UpdateCart();
        
        await _service.TransformShoppingCartToOrderAsync(_testuser1Id, "This is a test address #1");

        var order = await _context.Orders.Where(o => o.UserId == _testuser1Id).FirstOrDefaultAsync();
        
        Assert.That(order, Is.Not.Null);
        Assert.That(order.Address, Is.EqualTo("This is a test address #1"));
        Assert.That(order, Has.Count.EqualTo(shoppingCart.Count));
        Assert.That(order.TotalPrice, Is.EqualTo(shoppingCart.TotalPrice));
        Assert.That(game1!.Quantity, Is.EqualTo(98));
        Assert.That(game2!.Quantity, Is.EqualTo(99));
        Assert.That(user!.MagicPoints, Is.EqualTo(1000 + 140 * 100));

    }
    [Test]
    public async Task TransformShoppingCartToOrderAsync_ShouldCreateOrderWithCorrectParameters_AndUpdateValueMagicPointsWithDiscount()
    {
        var shoppingCart = await _context.ShoppingCarts
            .Where(sh => sh.UserId == _testuser2Id).FirstOrDefaultAsync();
        
        var game2 = await _repository.GetByIdAsync<Game>(2);

        var user = await _repository.GetByIdAsync<ApplicationUser>(_testuser2Id);
        
        shoppingCart!.UpdateCart();
        
        await _service.TransformShoppingCartToOrderAsync(_testuser2Id, "This is a test address #1");

        var order = await _context.Orders.Where(o => o.UserId == _testuser2Id).FirstOrDefaultAsync();
        
        Assert.That(order, Is.Not.Null);
        Assert.That(order.Address, Is.EqualTo("This is a test address #1"));
        Assert.That(order, Has.Count.EqualTo(shoppingCart.Count));
        Assert.That(order.TotalPrice, Is.EqualTo(shoppingCart.TotalPrice));
        Assert.That(game2!.Quantity, Is.EqualTo(99));
        Assert.That(user!.MagicPoints, Is.EqualTo(3800)); // 5000 - 5000 for discount and (40 - 40 * 5%) * 100 for purchase

    }
}