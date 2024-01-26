using ClothingStore.Core.Domain.Entities;

namespace ClothingStore.Core.DTO.ClothingVariants
{
    public class ClothingVariantUpdateRequest
    {
        public Guid Id { get; set; }
        public string? Image { get; set; }
        public string? Color { get; set; }
        public Size? Size { get; set; }
		public int? Stock { get; set; }

	}
}
