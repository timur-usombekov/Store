using ClothingStore.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Core.DTO.ClothingVariants
{
    public class ClothingVariantAddRequest
    {
        [Required(ErrorMessage = "Name for clothing variant can not be empty")]
        public string? Name { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Name for clothing variant can not be empty")]
        public string? Color { get; set; }
        public Size Size { get; set; }
    }
}
