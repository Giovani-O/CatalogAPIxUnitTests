using CatalogAPI.Controllers;
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

    // Testes de unidade usam a annotation [Fact]
    [Fact]
    public async Task GetProductById_Okresult()
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
}
