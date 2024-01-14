using ClothingStore.Core.DTO.Clothing;
using ClothingStore.Core.DTO.Customer;

namespace ClothingStore.Core.DTO.Review
{
	public class ReviewResponse
	{
		public ClothingResponse Clothing { get; set; }
		public CustomerResponse Customer { get; set; }
		public string? Comment { get; set; }
		public double Rating { get; set; }
	}
}
