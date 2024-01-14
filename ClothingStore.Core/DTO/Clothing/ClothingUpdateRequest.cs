using ClothingStore.Core.Domain.Entities;

namespace ClothingStore.Core.DTO.Clothing
{
    public class ClothingUpdateRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public Category? Category { get; set; }
        public string? Brand { get; set; }
        public int? Stock { get; set; }
    }
}
