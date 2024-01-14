using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClothingStore.Core.Domain.Entities
{
    public class Clothing
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
		public string? Description { get; set; }
		[Range(0, double.MaxValue)]
        public double Price { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public Category Category { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
        public string? Brand { get; set; }
        public int Stock { get; set; }
        public double Rating { get; set; }

        // Navigation property
        public ICollection<Review> Reviews { get; set; } = null!;
        public ICollection<ClothingVariant> ClothingVariants { get; set; } = null!;

    }

    public enum Category
    {
		Tshirts, 
        Pants, 
        Dresses, 
        Shoes
	}

}