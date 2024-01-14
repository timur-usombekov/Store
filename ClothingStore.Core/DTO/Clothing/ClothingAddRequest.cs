using ClothingStore.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Core.DTO.Clothing
{
    public class ClothingAddRequest
    {
        [Required(ErrorMessage = "Name can not be empty")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price can not be less than 0")]
        public double Price { get; set; }
        public Category Category { get; set; }
        public string? Brand { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Stock can not be less than 0")]
        public int Stock { get; set; }
    }
}
