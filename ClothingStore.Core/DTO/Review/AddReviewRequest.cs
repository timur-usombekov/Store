using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Core.DTO.Review
{
	public class AddReviewRequest
	{
		[Required(ErrorMessage = "Clothing id can not be null")]
		public Guid ClothingId { get; set; }
		[Required(ErrorMessage = "Clothing id can not be null")]
		public Guid CustomerId { get; set; }
		public string? Comment { get; set;}
		[Range(0, 5, ErrorMessage = "Rating can not be less than zero or above than five")]
		public double Rating { get; set; }
	}
}
