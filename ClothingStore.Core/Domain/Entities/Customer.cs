﻿
namespace ClothingStore.Core.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        // Navigation property
        public ICollection<Order> Orders { get; set; } = null!;
    }
}
