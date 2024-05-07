using CatalogAPI.Controllers;
using CatalogAPI.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPIxUnitTests.UnitTests;

// Herança de ProductsUnitTestController
public class GetProductUnitTests : IClassFixture<ProductsUnitTestController>
{
    // Injeta dependência _controller na classe
    private readonly ProductsController _controller;

    public GetProductUnitTests(ProductsUnitTestController controller)
    {
        _controller = new ProductsController(controller.repository, controller.mapper);
    }

    /// <summary>
    /// Testa resultado OK na busca por ID
    /// </summary>
    // Testes de unidade usam a annotation [Fact]
    [Fact]
    public async Task GetProductById_OkResult()
    {
        // Arrange
        var productId = 2;

        // Act
        var data = await _controller.Get(productId);

        // Assert usando xUnit
        //var okResult = Assert.IsType<OkObjectResult>(data.Result);
        //Assert.Equal(200, okResult.StatusCode);

        // Assert usando FluentAssertion
        data.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(200);
    }

    /// <summary>
    /// Testa resultado NotFould na busca por Id
    /// </summary>
    [Fact]
    public async Task GetproductById_NotFound()
    {
        var productId = 9999;

        var data = await _controller.Get(productId);

        data.Result.Should()
            .BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be(404);
    }

    /// <summary>
    /// Testa resultado BadRequest na busca por Id
    /// </summary>
    [Fact]
    public async Task GetProductById_BadRequest()
    {
        var productId = -1;

        var data = await _controller.Get(productId);

        data.Result.Should()
            .BeOfType<BadRequestObjectResult>()
            .Which.StatusCode.Should().Be(400);
    }

    /// <summary>
    /// Testa tipo de retorno na busca de todos os produtos
    /// </summary>
    [Fact]
    public async Task GetProducts_Return_ListOfProductsDTO()
    {
        var data = await _controller.Get();

        data.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IEnumerable<ProductDTO>>()
            .And.NotBeNull();
    }

    /// <summary>
    /// Testa resultado BadRequest na busca de todos os produtos
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetProducts_Return_BadRequest()
    {
        var data = await _controller.Get();

        data.Result.Should().BeOfType<BadRequestResult>();
    }
}
