using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Services;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;

namespace Tests;

public class ShoppingServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TransformShoppingCartItemToOrderItemAsync_CallsRepositoryAddAsync_AndModifiesQuantity()
    {
        var mockRepo = new Mock<IRepository>();
        var mockCachePointsService = new Mock<ICachePointsService>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<OrderItem>())).Returns(Task.CompletedTask);
        
        var shoppingService = new ShoppingService(mockRepo.Object, mockCachePointsService.Object);
        
        var shoppingCartItem = new ShoppingCartItem()
        {
            GameId = 1, 
            ShoppingCartId = 1, 
            Quantity = 2,
            Game = new Game {Quantity = 10}
        };

        int orderId = 100;

        var orderItem = await shoppingService.TransformShoppingCartItemToOrderItemAsync(shoppingCartItem, orderId);
        
        Assert.IsNotNull(orderItem);
        Assert.That(orderItem.GameId, Is.EqualTo(shoppingCartItem.GameId));
        Assert.That(orderItem.Quantity, Is.EqualTo(shoppingCartItem.Quantity));
        Assert.That(orderItem.OrderId, Is.EqualTo(orderId));

        // Verify that the repository's AddAsync method was called once with the correct OrderItem.
        mockRepo.Verify(repo => repo.AddAsync(It.Is<OrderItem>(oi =>
            oi.GameId == shoppingCartItem.GameId &&
            oi.Quantity == shoppingCartItem.Quantity &&
            oi.OrderId == orderId
        )), Times.Once);

        // Assert: Ensure the Game quantity was reduced.
        Assert.That(shoppingCartItem.Game.Quantity, Is.EqualTo(8));
    }
}