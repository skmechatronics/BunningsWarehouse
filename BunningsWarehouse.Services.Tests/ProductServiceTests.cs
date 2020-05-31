using BunningsWarehouse.Data;
using BunningsWarehouse.Data.Entities;
using BunningsWarehouse.Services.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace BunningsWarehouse.Services.Tests
{
	[TestFixture]
	public class ProductServiceTests
	{
		private BaseDbContext CreateInMemoryContext(string databaseName)
		{
			var options = new DbContextOptionsBuilder<BaseDbContext>()
				.UseInMemoryDatabase(databaseName: databaseName)
				.Options;

			return new BaseDbContext(options);
		}

		[Test]
		public async Task When_a_product_exists_expect_product_is_not_added()
		{
			var context = CreateInMemoryContext("When_a_product_exists_expect_product_is_not_added");
			context.Products.Add(new ProductEntity { Name = "Screwdriver" });
			context.SaveChanges();

			var productsService = new ProductService(context);
			await productsService.AddProduct("Screwdriver");
			context.Products.Count().Should().Be(1);
		}

		[Test]
		public async Task When_a_product_exists_expect_same_lowercased_product_is_not_added()
		{
			var context = CreateInMemoryContext("When_a_product_exists_expect_same_lowercased_product_is_not_added");
			context.Products.Add(new ProductEntity { Name = "Screwdriver" });
			context.SaveChanges();

			var productsService = new ProductService(context);
			await productsService.AddProduct("screwdriver");
			context.Products.Count().Should().Be(1);
		}

		[Test]
		public async Task When_retrieving_products_expect_number_of_products_that_were_added()
		{
			var context = CreateInMemoryContext("When_retrieving_products_expect_number_of_products_that_were_added");
			context.Products.Add(new ProductEntity { Name = "Screwdriver" });
			context.Products.Add(new ProductEntity { Name = "Hammer" });
			context.Products.Add(new ProductEntity { Name = "Saw" });
			context.SaveChanges();

			var productsService = new ProductService(context);
			var products = await productsService.GetAllProducts();
			products.Count().Should().Be(3);
		}

		[Test]
		public async Task When_a_product_name_is_empty_expect_error()
		{
			var context = CreateInMemoryContext("When_a_product_name_is_empty_expect_error");

			var productsService = new ProductService(context);
			var result = await productsService.AddProduct("  ");
			context.Products.Count().Should().Be(0);
			result.Errors.FirstOrDefault()?.Code.Should().Be((int)ErrorCode.ValidationError);
		}

		[Test]
		public async Task When_updating_a_product_below_its_quantity_expect_error()
		{
			var context = CreateInMemoryContext("When_updating_a_product_below_its_quantity_expect_error");
			context.Products.Add(new ProductEntity { Name = "Screwdriver", Quantity = 10});
			context.SaveChanges();

			var productService = new ProductService(context);
			var result = await productService.UpdateProduct("Screwdriver", -15);
			result.Errors.FirstOrDefault().Code.Should().Be((int)ErrorCode.NotEnoughQuantity);
		}
	}
}
