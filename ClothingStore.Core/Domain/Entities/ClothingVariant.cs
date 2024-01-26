using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Core.Domain.Entities
{
	public class ClothingVariant
	{
		[Key]
		public Guid Id { get; set; }
		public Guid ClothingId { get; set; }
		public Guid OrderDetailId { get; set; }
		public string? Image { get; set; }
		public string Color { get; set; } = null!;
		public int Stock { get; set; }
		public Size Size { get; set; }

		[ForeignKey(nameof(ClothingId))]
		public Clothing Clothing { get; set; } = null!;
		[ForeignKey(nameof(OrderDetailId))]
		public ICollection<OrderDetail> OrderDetails { get; set; } = null!;

	}

	public enum Size
	{
		S,
		M,
		L,
		XL
	}
}
