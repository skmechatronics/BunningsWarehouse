using BunningsWarehouse.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BunningsWarehouse.Api.Controllers
{
	public abstract class AbstractController : ControllerBase
	{
		protected async Task<ActionResult<ApiResult>> HandleApiResult(Func<Task<ApiResult>> action)
		{
			var result = await action.Invoke();
			if (result.Errors.Any())
			{
				return new BadRequestObjectResult(result);
			}

			return new OkObjectResult(result);
		}

	}
}
