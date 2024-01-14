using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Core.DTO.Customer
{
	public class AddCustomerRequest
	{
		[Required(ErrorMessage = "Name can not be null")]
		public string Name { get; set; } = null!;
		[Required(ErrorMessage = "Email can not be null")]
		public string Email { get; set; } = null!;
	}
}
