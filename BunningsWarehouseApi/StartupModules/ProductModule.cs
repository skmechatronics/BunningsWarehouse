using BunningsWarehouse.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BunningsWarehouse.Api.StartupModules
{
	public class ProductModule : IStartupModule
	{
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<IProductService, ProductService>();
		}
	}
}
