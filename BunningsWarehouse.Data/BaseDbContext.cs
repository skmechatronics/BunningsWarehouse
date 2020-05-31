using BunningsWarehouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BunningsWarehouse.Data
{
	public class BaseDbContext : DbContext
	{

		public BaseDbContext(DbContextOptions options)
			: base(options)
		{
		}

		public virtual DbSet<ProductEntity> Products { get; set; }
	}
}
