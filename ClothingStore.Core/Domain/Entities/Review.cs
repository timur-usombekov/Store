using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Core.Domain.Entities
{
	public class Review
	{
		[Key]
		public Guid Id { get; set; }
		public Guid ClothisId { get; set; }
		public Guid CustomerId { get; set; }
		public double Rating { get; set; }
		public string? Comment { get; set; }

		[ForeignKey(nameof(CustomerId))]
		public Customer Customer { get; set; } = null!;
		[ForeignKey(nameof(ClothisId))]
		public Clothing Clothing { get; set; } = null!;
	}
}
