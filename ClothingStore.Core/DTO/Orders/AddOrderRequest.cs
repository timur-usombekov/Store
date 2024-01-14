using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Core.DTO.Orders
{
	public class AddOrderRequest
	{
		[Required(ErrorMessage = "Customer Id can not be empty")]
		public Guid CustomerId { get; set; }
	}
}
