using BunningsWarehouse.Data.Models;
using BunningsWarehouse.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BunningsWarehouse.Services
{
	public interface IProductService
	{
		Task<ApiResult> AddProduct(string productName);

		Task<IEnumerable<Product>> GetAllProducts();

		Task<ApiResult<Product>> GetProductById(int id);

		Task<ApiResult> UpdateProduct(string productName, int quantity);

		Task<ApiResult> DeleteProduct(int id);
	}
}
