using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BunningsWarehouse.Api.StartupModules
{
	public interface IStartupModule
	{
		void Configure(IApplicationBuilder app, IWebHostEnvironment env);

		void ConfigureServices(IServiceCollection services);
	}
}