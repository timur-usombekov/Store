using ClothingStore.Core.Domain.Entities;

namespace ClothingStore.Core.DTO.Clothing
{
    public class FilterClothingDTO
    {
        public string? Name { get; set; }
        public double? MinimalCost { get; set; }
        public double? MaximumCost { get; set; }
        public Category? Category { get; set; }
        public string? Color { get; set; }
        public Size? Size { get; set; }
        public string? Brand { get; set; }
        public double? Rating { get; set; }
    }
}
