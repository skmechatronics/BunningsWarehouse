using System.Collections.Generic;
using System.Linq;

namespace BunningsWarehouse.Services.Models
{
	public class ApiResult
	{
		public ApiResult()
		{
		}

		public ApiResult(params Error[] errors)
		{
			this.Errors = errors.ToList();
		}

		public List<Error> Errors { get; set; } = new List<Error>();
	}
}
