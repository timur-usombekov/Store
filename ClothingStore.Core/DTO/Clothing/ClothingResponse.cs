using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.DTO.ClothingVariants;

namespace ClothingStore.Core.DTO.Clothing
{
    public class ClothingResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public string? Brand { get; set; }
        public int TotalStock { get; set; }
        public double Rating { get; set; }
        public ICollection<ClothingVariantResponse>? clothingVariants { get; set; }
    }
}
