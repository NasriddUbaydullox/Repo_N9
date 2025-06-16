using FluentAssertions;
using Moq;
using Moq.AutoMock;
using OrderService.DataAccess.Entities;
using OrderService.Dtos;
using OrderService.Mediator.Order.CreateOrder;
using OrderService.ProductClients;
using OrderService.Reposiotries.Interfaces;

namespace OrderServiceTests
{
    public class UnitTest1
    {

        [Fact]
        public async Task Handle_WithValidProduct_ShouldCreateOrder()
        {
            // Arrange
            var mocker = new AutoMocker();

            var product = new ProductDto { Id = 1, Name = "iPhone", Stock = 10 };
            var command = new CreateOrderCommand(new CreateOrderDto
            {
                ProductId = 1,
                Quantity = 5,
                CreatedAt = DateTime.UtcNow
            });

            mocker.GetMock<IProductClient>()
                .Setup(p => p.GetProductByIdAsync(1))
                .ReturnsAsync(product);

            mocker.GetMock<IProductClient>()
                .Setup(p => p.UpdateProductStockAsync(1, 5))
                .ReturnsAsync(true);

            var testOrder = new Order
            {
                Id = 123, 
                ProductId = 1,
                Quantity = 5,
                CreatedAt = DateTime.UtcNow
            };

            mocker.GetMock<IOrderRepository>()
                .Setup(r => r.AddAsync(It.IsAny<Order>()))
                .Callback<Order>(o =>
                {
                    o.Id = testOrder.Id;
                })
                .Returns(Task.CompletedTask);

            var handler = mocker.CreateInstance<CreateOrderCommandHandler>();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(123); 
            result.Should().BeGreaterThan(0);

            mocker.GetMock<IOrderRepository>()
                .Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithInsufficientStock_ShouldThrow()
        {
            // Arrange
            var mocker = new AutoMocker();

            var command = new CreateOrderCommand(new CreateOrderDto
            {
                ProductId = 1,
                Quantity = 20
            });

            mocker.GetMock<IProductClient>()
                .Setup(p => p.GetProductByIdAsync(1))
                .ReturnsAsync(new ProductDto { Id = 1, Stock = 10 });

            var handler = mocker.CreateInstance<CreateOrderCommandHandler>();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNonexistentProduct_ShouldThrow()
        {
            // Arrange
            var mocker = new AutoMocker();

            var command = new CreateOrderCommand(new CreateOrderDto
            {
                ProductId = 999,
                Quantity = 1
            });

            mocker.GetMock<IProductClient>()
                .Setup(p => p.GetProductByIdAsync(999))
                .ReturnsAsync((ProductDto?)null);

            var handler = mocker.CreateInstance<CreateOrderCommandHandler>();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}