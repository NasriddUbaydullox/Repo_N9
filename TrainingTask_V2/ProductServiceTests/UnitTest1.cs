using FluentAssertions;
using Moq;
using Moq.AutoMock;
using ProductService.DataAccess.Entities;
using ProductService.Dtos;
using ProductService.Mediator.Products.CreateProduct;
using ProductService.Mediator.Products.GetProductById;
using ProductService.Mediator.Products.UpdateStockById;
using ProductService.Reposiotries.Interfaces;

namespace ProductServiceTests;

public class UnitTest1
{
    private readonly AutoMocker _mocker = new AutoMocker();

    [Fact]
    public async Task CreateProductHandler_WithValidProduct_ShouldCreateAndReturnId()
    {
        // Arrange
        var command = new CreateProductCommand(new CreateProductDto
        {
            Name = "Test Product",
            Stock = 10
        });

        var testProduct = new Product
        {
            Id = 1,
            Name = "Test Product",
            Stock = 10
        };

        _mocker.GetMock<IProductRepository>()
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .Callback<Product>(p => p.Id = testProduct.Id)
            .Returns(Task.CompletedTask);

        var handler = _mocker.CreateInstance<CreateProductCommandHandler>();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(1);
        _mocker.GetMock<IProductRepository>()
            .Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task CreateProductHandler_WithRepositoryError_ShouldThrow()
    {
        // Arrange
        var command = new CreateProductCommand(new CreateProductDto
        {
            Name = "Test Product",
            Stock = 10
        });

        _mocker.GetMock<IProductRepository>()
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ThrowsAsync(new Exception("Database error"));

        var handler = _mocker.CreateInstance<CreateProductCommandHandler>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            handler.Handle(command, CancellationToken.None));
    }
    [Fact]
    public async Task GetProductByIdHandler_WithExistingProduct_ShouldReturnProduct()
    {
        // Arrange
        var query = new GetProductByIdQuery(1);

        var testProduct = new Product
        {
            Id = 1,
            Name = "Test Product",
            Stock = 10
        };

        _mocker.GetMock<IProductRepository>()
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(testProduct);

        var handler = _mocker.CreateInstance<GetProductByIdQueryHandler>();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Test Product");
        result.Stock.Should().Be(10);
    }

    [Fact]
    public async Task GetProductByIdHandler_WithNonExistingProduct_ShouldThrow()
    {
        // Arrange
        var query = new GetProductByIdQuery(999);

        _mocker.GetMock<IProductRepository>()
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Product?)null);

        var handler = _mocker.CreateInstance<GetProductByIdQueryHandler>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            handler.Handle(query, CancellationToken.None));
    }
    [Fact]
    public async Task UpdateStockHandler_WithExistingProduct_ShouldUpdateStock()
    {
        // Arrange
        var command = new UpdateStockByIdCommand(1, 15);

        var testProduct = new Product
        {
            Id = 1,
            Name = "Test Product",
            Stock = 10
        };

        _mocker.GetMock<IProductRepository>()
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(testProduct);

        var handler = _mocker.CreateInstance<UpdateStockByIdCommandHandler>();

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        testProduct.Stock.Should().Be(15);
        _mocker.GetMock<IProductRepository>()
            .Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateStockHandler_WithNonExistingProduct_ShouldThrow()
    {
        // Arrange
        var command = new UpdateStockByIdCommand(999, 15);

        _mocker.GetMock<IProductRepository>()
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Product?)null);

        var handler = _mocker.CreateInstance<UpdateStockByIdCommandHandler>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            handler.Handle(command, CancellationToken.None));
    }
}