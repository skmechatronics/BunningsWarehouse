namespace BunningsWarehouse.Services.Models
{
	public class ApiResult<T> : ApiResult
	{
		public ApiResult(params Error[] errors)
			: base(errors)
		{

		}

		public ApiResult(T data)
		{
			this.Data = data;
		}

		public T Data { get; set; }
	}
}
