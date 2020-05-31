using BunningsWarehouse.Api.Controllers;
using BunningsWarehouse.Data.Models;
using BunningsWarehouse.Services;
using BunningsWarehouse.Services.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BunningsWarehouse.Api.Tests
{
	[TestFixture]
	public class ProductControllerTests
	{
		// Demonstrate mocking and return type checking for negative test case.
		[Test]
		public async Task When_adding_a_product_an_error_occurs_expect_bad_request()
		{
			var duplicateProduct = new ApiResult(new Error(ErrorCode.ProductNotFound, "Message"));
			var mockedService = Mock.Of<IProductService>();
			Mock.Get(mockedService).Setup(i => i.AddProduct(It.IsAny<string>())).ReturnsAsync(duplicateProduct);

			var controller = new ProductController(mockedService);
			var result = await controller.AddItem("DuplicateProduct");
			result.Result.Should().BeEquivalentTo(new BadRequestObjectResult(duplicateProduct));
		}


		// Demonstrate mocking and return type checking for positive test case.
		[Test]
		public async Task When_adding_a_product_succeeds_expect_ok_object_result()
		{
			var notFoundApiResult = new ApiResult();
			var mockedService = Mock.Of<IProductService>();
			Mock.Get(mockedService).Setup(i => i.AddProduct(It.IsAny<string>())).ReturnsAsync(notFoundApiResult);

			var controller = new ProductController(mockedService);
			var result = await controller.AddItem("NotFoundProduct");
			result.Result.Should().BeEquivalentTo(new OkObjectResult(notFoundApiResult));
		}


		// Demonstrate use of verify
		[Test]
		public async Task When_updating_a_product_expect_name_and_quantity_to_be_supplied()
		{
			var mockedService = Mock.Of<IProductService>();
			Mock.Get(mockedService).Setup(i => i.UpdateProduct(It.IsAny<string>(), It.IsAny<int>()))
				.ReturnsAsync(new ApiResult());
			var controller = new ProductController(mockedService);
			
			var updatedProduct = new Product { Name = "Screwdriver", Quantity = 20 };
			var result = await controller.Update(updatedProduct);
			Mock.Get(mockedService).Verify(i => i.UpdateProduct("Screwdriver", 20));
		}
	}
}
