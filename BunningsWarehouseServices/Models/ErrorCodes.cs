namespace BunningsWarehouse.Services.Models
{
	public enum ErrorCode
	{
		ProductNotFound = 1000,

		ProductAlreadyExists,

		ValidationError,

		NotEnoughQuantity,
		ProductNotEmptied
	}
}
