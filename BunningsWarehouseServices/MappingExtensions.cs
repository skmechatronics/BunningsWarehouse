using BunningsWarehouse.Data.Entities;
using BunningsWarehouse.Data.Models;

namespace BunningsWarehouse.Services
{
	public static class MappingExtensions
	{
		public static ProductEntity FromModel(this Product product)
		{
			return new ProductEntity
			{
				Id = product.Id,
				Name = product.Name,
				Quantity = product.Quantity
			};
		}

		public static Product ToModel(this ProductEntity entity)
		{
			return new Product
			{
				Id = entity.Id,
				Name = entity.Name,
				Quantity = entity.Quantity
			};
		}
	}
}
