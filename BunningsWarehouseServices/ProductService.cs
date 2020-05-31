using BunningsWarehouse.Data;
using BunningsWarehouse.Data.Entities;
using BunningsWarehouse.Data.Models;
using BunningsWarehouse.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunningsWarehouse.Services
{
	public class ProductService : IProductService
	{
		private readonly BaseDbContext dbContext;

		public ProductService(BaseDbContext dbContext)
		{
			this.dbContext = dbContext;
		}


		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			return await this.dbContext.Products.Select(product => product.ToModel()).ToListAsync();
		}


		public async Task<ApiResult> AddProduct(string productName)
		{
			var trimmed = productName.Trim();
			if (string.IsNullOrEmpty(trimmed))
			{
				var error = new Error(ErrorCode.ValidationError, "Product name cannot be null or empty");
				return new ApiResult(error);
			}

			return await ValidateExistingAndCreate(trimmed);
		}


		public async Task<ApiResult<Product>> GetProductById(int id)
		{
			var existing = await this.dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
			if (existing != null)
			{
				return new ApiResult<Product>(existing.ToModel());
			}

			var error = new Error(ErrorCode.ProductNotFound, $"The product with id {id} was not found");
			return new ApiResult<Product>(error);
		}


		public async Task<ApiResult> UpdateProduct(string productName, int quantity)
		{
			var existingProduct = await GetProduct(productName);
			if (existingProduct == null)
			{
				var error = new Error(ErrorCode.ProductNotFound, $"The product with name {productName} was not found");
				return new ApiResult(error);
			}

			return await this.ValidateQuantity(productName, quantity, existingProduct);
		}

		
		public async Task<ApiResult> DeleteProduct(int id)
		{
			var existing = await this.dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
			if (existing == null)
			{
				var error = new Error(ErrorCode.ProductNotFound, $"The product with {id} was not found");
				return new ApiResult(error);
			}

			return await CheckEmptied(existing);
		}


		private async Task<ApiResult> CheckEmptied(ProductEntity product)
		{
			if (product.Quantity > 0)
			{
				var error = new Error(ErrorCode.ProductNotEmptied, $"The product {product.Name} still has existing stock. Reduce quantity to zero first.");
				return new ApiResult(error);
			}

			this.dbContext.Remove(product);
			await this.dbContext.SaveChangesAsync();

			return new ApiResult();
		}


		private async Task<ApiResult> ValidateExistingAndCreate(string productName)
		{
			var existing = await GetProduct(productName);
			if (existing != null)
			{
				var error = new Error(ErrorCode.ProductAlreadyExists, $"The product with name {productName} already exists");
				return new ApiResult(error);
			}

			return await CreateProduct(productName);
		}


		private async Task<ApiResult> CreateProduct(string productName)
		{
			var product = new Product
			{
				Name = productName,
				Quantity = 0
			};

			this.dbContext.Products.Add(product.FromModel());
			await this.dbContext.SaveChangesAsync();
			return new ApiResult();
		}


		private async Task<ProductEntity> GetProduct(string productName)
		{
			productName = productName.Trim();
			var existingProduct = await this.dbContext.Products.FirstOrDefaultAsync(
							entity => EF.Functions.Like(entity.Name, $"%{productName}%"));

			return existingProduct;
		}


		private async Task<ApiResult> ValidateQuantity(string productName, int quantity, ProductEntity existingProduct)
		{
			if (quantity + existingProduct.Quantity < 0)
			{
				var error = new Error(ErrorCode.NotEnoughQuantity, $"Insufficient quantity for product {productName}");
				return new ApiResult(error);
			}

			existingProduct.Quantity += quantity;
			this.dbContext.Attach(existingProduct);
			await this.dbContext.SaveChangesAsync();

			return new ApiResult();
		}

	}
}
