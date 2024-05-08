using CatalogAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPIxUnitTests.UnitTests;

public class DeleteProductUnitTests : IClassFixture<ProductsUnitTestController>
{
    private readonly ProductsController _controller;

    public DeleteProductUnitTests(ProductsUnitTestController controller)
    {
        _controller = new ProductsController(controller.repository, controller.mapper);
    }

    /// <summary>
    /// Testa resultado OkResult ao excluir um produto
    /// </summary>
    [Fact]
    public async Task DeleteProduct_Result_OkResult()
    {
        var productId = 12; // Certifique-se de que o produto existe para passar no teste

        var result = await _controller.Delete(productId);

        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    /// <summary>
    /// Testa resultado NotFound ao excluir um produto
    /// </summary>
    [Fact]
    public async Task DeleteProduct_Result_NotFound()
    {
        var productId = 9999;

        var result = await _controller.Delete(productId);

        result.Should().NotBeNull();
        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }


    /// <summary>
    /// Testa resultado BadRequest ao excluir um produto
    /// </summary>
    [Fact]
    public async Task DeleteProduct_Result_BadRequest()
    {
        var productId = -1;

        var result = await _controller.Delete(productId);

        result.Should().NotBeNull();
        result.Result.Should().BeOfType<BadRequestResult>();
    }
}
