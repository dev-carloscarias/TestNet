using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MediatR;
using Prueba_NET.Application.Interfaces;
using Prueba_NET.Application.Queries;
using Prueba_NET.Domain.Entities;
using PruebaNET_CarlosCarias_API.Controllers;


public class ProductControllerTests
{
    private readonly Mock<ILogger<ProductController>> _mockLogger;
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<IProductStatusService> _mockStatusService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockLogger = new Mock<ILogger<ProductController>>();
        _mockMediator = new Mock<IMediator>();
        _mockStatusService = new Mock<IProductStatusService>();

        _controller = new ProductController(_mockLogger.Object, _mockMediator.Object, _mockStatusService.Object);
    }

    [Fact]
    public async Task Get_ReturnsProductWithStatusName()
    {
        var product = new Product { ProductId = 1, Name = "Test Product", Status = 1, StatusName = "Active" };
        var statuses = new Dictionary<int, string> { { 1, "Active" }, { 0, "Inactive" } };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default)).ReturnsAsync(product);
        _mockStatusService.Setup(s => s.GetProductStatuses()).Returns(statuses);

        var result = await _controller.Get(1) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var returnedProduct = result.Value as Product;
        Assert.Equal("Active", returnedProduct.StatusName);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenProductDoesNotExist()
    {
        _mockMediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default)).ReturnsAsync((Product)null);

        var result = await _controller.Get(1);
        Assert.IsType<NotFoundResult>(result);
    }
}
