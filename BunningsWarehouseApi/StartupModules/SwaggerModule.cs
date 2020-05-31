using BunningsWarehouse.Api.StartupModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BunningsWarehouse.Api
{
	public class SwaggerModule : IStartupModule
	{
		private const string ApiName = "Bunnings Warehouse Inventory";

		private const string Version = "v1.0";

		private const string SwaggerJsonPath = "/swagger/v1/swagger.json";

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint(SwaggerJsonPath, ApiName);
			});
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = ApiName, Version = Version });
			});
		}
	}
}
