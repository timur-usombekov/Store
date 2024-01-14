using ClothingStore.Core.DTO.Customer;
using ClothingStore.Core.DTO.OrderDetail;

namespace ClothingStore.Core.DTO.Orders
{
	public class OrderResponse
	{
		public Guid Id { get; set; }
		public CustomerResponse Customer { get; set; } = null!;
		public DateTime OrderDate { get; set; }
		public ICollection<OrderDetailResponse> OrderDetails { get; set; } = null!;
	}
}
