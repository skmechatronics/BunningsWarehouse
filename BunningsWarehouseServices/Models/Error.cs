namespace BunningsWarehouse.Services.Models
{
	public class Error
	{
		public Error(ErrorCode code, string message)
		{
			this.Code = (int)code;
			this.Message = message;
		}

		public int Code { get; }

		public string Message { get; }
	}
}
