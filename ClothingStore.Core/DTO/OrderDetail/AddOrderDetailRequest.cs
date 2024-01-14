using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Core.DTO.OrderDetail
{
	public class AddOrderDetailRequest
	{
		[Required(ErrorMessage = "Order Id can not be empty")]
		public Guid OrderID { get; set; }
		[Required(ErrorMessage = "Clothing Variant Id can not be empty")]
		public Guid ClothingVariantId { get; set; }
		[Range(0,int.MaxValue, ErrorMessage = "Quantity can not be less than 0")]
		public int Quantity { get; set; }
	}
}
