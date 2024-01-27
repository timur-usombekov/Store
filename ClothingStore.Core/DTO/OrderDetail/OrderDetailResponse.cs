using ClothingStore.Core.DTO.ClothingVariants;

namespace ClothingStore.Core.DTO.OrderDetail
{
    public class OrderDetailResponse
    {
		public Guid Id { get; set; }
		public ClothingVariantResponse ClothingVariant { get; set; } = null!;
		public int Quantity { get; set; }
	}
}
