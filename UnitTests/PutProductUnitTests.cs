using CatalogAPI.Controllers;
using CatalogAPI.DTOs;
using CatalogAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPIxUnitTests.UnitTests;

public class PutProductUnitTests : IClassFixture<ProductsUnitTestController>
{
    private readonly ProductsController _controller;

    public PutProductUnitTests(ProductsUnitTestController controller)
    {
        _controller = new ProductsController(controller.repository, controller.mapper);
    }

    /// <summary>
    /// Testa resultado OkResult na edição de produtos
    /// </summary>
    [Fact]
    public async Task PutProduct_Return_OkResult()
    {
        var productId = 3;

        var updatedProductDTO = new ProductDTO
        {
            Id = productId,
            Name = "Açaí - edit",
            Description = "Copo 400ml com acompanhamentos personalizados",
            ImageUrl = "acai1.jpg",
            CategoryId = 3
        };

        var result = await _controller.Put(productId, updatedProductDTO) as ActionResult<ProductDTO>;

        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task PutProduct_Return_BadRequest()
    {
        var producitId = 9999;

        var product = new ProductDTO
        {
            Id = 3,
            Name = "Açaí - edit",
            Description = "Copo 400ml com acompanhamentos personalizados",
            ImageUrl = "acai1.jpg",
            CategoryId = 3
        };

        var data = await _controller.Put(producitId, product);

        data.Result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
    }
}
