using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Services;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class ShoppingServiceTests
{
    private Mock<IRepository> _mockRepo;
    private Mock<ICachePointsService> _mockCachePointsService;
    private ShoppingService _shoppingService;
    
    private readonly Guid _userId = Guid.NewGuid();
    private readonly string _address = "Sofia str Ivan Vazov #29";
    
    
    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IRepository>();
        _mockCachePointsService = new Mock<ICachePointsService>();
        _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<OrderItem>())).Returns(Task.CompletedTask);
        _shoppingService = new ShoppingService(_mockRepo.Object, _mockCachePointsService.Object);

        ApplicationUser user = new ApplicationUser()
        {
            Id = _userId,
            Address = _address,
            MagicPoints = 100
        };

        _mockRepo.Setup(repo => repo.GetByIdAsync<ApplicationUser>(_userId))
            .ReturnsAsync(user);
    }

    [Test]
    public async Task TransformShoppingCartItemToOrderItemAsync_CallsRepositoryAddAsync_AndModifiesQuantity()
    {
        var shoppingCartItem = new ShoppingCartItem()
        {
            GameId = 1, 
            ShoppingCartId = 1, 
            Quantity = 2,
            Game = new Game {Quantity = 10}
        };

        int orderId = 100;

        var orderItem = await _shoppingService.TransformShoppingCartItemToOrderItemAsync(shoppingCartItem, orderId);
        
        Assert.That(orderItem, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(orderItem.GameId, Is.EqualTo(shoppingCartItem.GameId));
            Assert.That(orderItem.Quantity, Is.EqualTo(shoppingCartItem.Quantity));
            Assert.That(orderItem.OrderId, Is.EqualTo(orderId));
        });

        // Verify that the repository's AddAsync method was called once with the correct OrderItem.
        _mockRepo.Verify(repo => repo.AddAsync(It.Is<OrderItem>(oi =>
            oi.GameId == shoppingCartItem.GameId &&
            oi.Quantity == shoppingCartItem.Quantity &&
            oi.OrderId == orderId
        )), Times.Once);

        // Assert: Ensure the Game quantity was reduced.
        Assert.That(shoppingCartItem.Game.Quantity, Is.EqualTo(8));
    }

    [Test]
    public async Task TransformShoppingCartToOrderAsync_WithNullShoppingCartShouldThrowInvalidOperationException()
    {
        var emptyCarts = new List<ShoppingCart>().AsQueryable();
        
        _mockRepo.Setup(repo => repo.AllReadOnly<ShoppingCart>())
            .Returns(emptyCarts);
        
        Assert.ThrowsAsync<InvalidOperationException>(() => 
            _shoppingService.TransformShoppingCartToOrderAsync(_userId, _address));
    }

    [Test]
    public async Task TransformShoppingCartToOrderAsync_WithNullUserShouldThrowInvalidOperationException()
    {
        Guid userId = Guid.NewGuid();

        _mockRepo.Setup(repo => repo.GetByIdAsync<ApplicationUser>(userId))
            .ReturnsAsync((ApplicationUser?)null);
        
        Assert.ThrowsAsync<InvalidOperationException>(() => 
            _shoppingService.TransformShoppingCartToOrderAsync(userId, _address));        
    }
}





