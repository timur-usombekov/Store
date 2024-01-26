using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.DTO.Clothing;

namespace ClothingStore.Core.DTO.ClothingVariants
{
    public class ClothingVariantResponse
    {
        public Guid Id { get; set; }
        public string ClothingName { get; set; } = null!;
        public string? Image { get; set; }
        public string Color { get; set; } = null!;
        public Size Size { get; set; }
		public int Stock { get; set; }

	}

}
