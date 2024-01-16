using ClothingStore.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Core.DTO.OrderDetail
{
	public class UpdateOrderDetailRequest
	{
		public Guid OrderDetailId { get; set; }
		public Guid? OrderId { get; set; }
		public Guid? ClothingVariantId { get; set; }
		public int? Quantity { get; set; }

	}
}
