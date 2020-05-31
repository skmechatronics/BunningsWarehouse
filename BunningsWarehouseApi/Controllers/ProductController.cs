using BunningsWarehouse.Data.Models;
using BunningsWarehouse.Services;
using BunningsWarehouse.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunningsWarehouse.Api.Controllers
{
	public class ProductController : AbstractController
	{
		private readonly IProductService productService;

		public ProductController(IProductService productService)
		{
			this.productService = productService;
		}

		[HttpPut]
		[Route("api/products")]
		public async Task<ActionResult<ApiResult>> AddItem(string productName)
		{
			async Task<ApiResult> addProduct() => await this.productService.AddProduct(productName);
			return await HandleApiResult(addProduct);
		}

		[HttpGet]
		[Route("api/products")]
		public async Task<ActionResult<IEnumerable<Product>>> GetAll()
		{
			var products = await this.productService.GetAllProducts();
			return new OkObjectResult(products);
		}

		[HttpGet]
		[Route("api/products/{id}")]
		public async Task<ActionResult<ApiResult>> GetById(int id)
		{
			async Task<ApiResult> getProductById() => await this.productService.GetProductById(id);
			return await HandleApiResult(getProductById);
		}

		[HttpPost]
		[Route("api/products")]
		public async Task<ActionResult<ApiResult>> Update(Product product)
		{
			async Task<ApiResult> updateProduct() => await this.productService.UpdateProduct(product.Name, product.Quantity);
			return await HandleApiResult(updateProduct);
		}

		[HttpDelete]
		[Route("api/products/{id}")]
		public async Task<ActionResult<ApiResult>> Delete(int id)
		{
			async Task<ApiResult> deleteProduct() => await this.productService.DeleteProduct(id);
			return await HandleApiResult(deleteProduct);
		}

	}
}
