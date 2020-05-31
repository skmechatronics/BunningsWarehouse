using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BunningsWarehouse.Data.Entities
{
	[Table("Products")]
	public class ProductEntity
	{
		[Key]
		public int Id { get; set; }


		public string Name { get; set; }
	

		public int Quantity { get; set; }
	}
}
