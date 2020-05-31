using BunningsWarehouse.Api;
using BunningsWarehouse.Api.StartupModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace BunningsWarehouseApi
{
	public class Startup
	{
		private readonly List<IStartupModule> StartupModules;
		
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			StartupModules = new List<IStartupModule>
				{
					new DefaultModule(),
					new SwaggerModule(),
					new DatabaseModule(configuration),
					new ProductModule()
				};
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			foreach (var module in StartupModules)
			{
				module.ConfigureServices(services);
			}
			
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			foreach(var module in StartupModules)
			{
				module.Configure(app, env);
			}
		}
	}
}
