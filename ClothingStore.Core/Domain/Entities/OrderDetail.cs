using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Core.Domain.Entities
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        // Foreign keys
        public Guid OrderID { get; set; }
        public Guid ClothingVariantId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ClothingVariantId))]
        public ClothingVariant ClothingVariant { get; set; } = null!;
		[ForeignKey(nameof(OrderID))]
		public Order Order { get; set; } = null!;


    }
}
