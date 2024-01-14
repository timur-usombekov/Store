using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Core.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation properties
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
		public ICollection<OrderDetail> OrderDetails { get; set; } = null!;

    }
}
