using CatalogAPI.Controllers;
using CatalogAPI.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPIxUnitTests.UnitTests
{
    public class PostProductUnitTests : IClassFixture<ProductsUnitTestController>
    {
        private readonly ProductsController _controller;

        public PostProductUnitTests(ProductsUnitTestController controller)
        {
            _controller = new ProductsController(controller.repository, controller.mapper);
        }

        /// <summary>
        /// Testa resultado CreatedAtRoute na criação de produtos
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostProduct_Return_CreatedStatusCode()
        {
            // Arrange
            var newProduct = new ProductDTO
            {
                Name = "Novo produto",
                Description = "Description...",
                Price = 10.99m,
                ImageUrl = "image.jpg",
                CategoryId = 3,
            };

            // Act
            var data = await _controller.Post(newProduct);

            // Assert
            var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task PostProduct_Return_BadRequest()
        {
            ProductDTO product = null;

            var data = await _controller.Post(product);

            var badRequestResult = data.Result.Should().BeOfType<BadRequestResult>();
            badRequestResult.Subject.StatusCode.Should().Be(400);
        }
    }
}
