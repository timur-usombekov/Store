using ClothingStore.Core.DTO.ClothingVariants;

namespace ClothingStore.Core.DTO.OrderDetail
{
    public class OrderDetailResponse
    {
		public ClothingVariantResponse ClothingVariant { get; set; } = null!;
		public int Quantity { get; set; }
	}
}
