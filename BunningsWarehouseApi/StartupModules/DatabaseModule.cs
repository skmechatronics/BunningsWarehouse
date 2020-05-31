using BunningsWarehouse.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BunningsWarehouse.Api.StartupModules
{
	public class DatabaseModule : IStartupModule
	{
		private readonly DbOptions dbOptions;

		public DatabaseModule(IConfiguration configuration)
		{
			this.dbOptions = configuration.GetSection(nameof(DbOptions)).Get<DbOptions>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<BaseDbContext>(options => 
				options.UseSqlServer(this.dbOptions.ConnectionString));
		}
	}
}
