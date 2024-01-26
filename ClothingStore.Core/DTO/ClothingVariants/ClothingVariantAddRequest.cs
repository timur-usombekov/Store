using ClothingStore.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Core.DTO.ClothingVariants
{
    public class ClothingVariantAddRequest
    {
        [Required(ErrorMessage = "Clothing id for clothing variant can not be empty")]
        public Guid ClothingId { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Color for clothing variant can not be empty")]
        public string Color { get; set; }
        public Size Size { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Stock can not be less than 0")]
		public int Stock { get; set; }

	}
}
