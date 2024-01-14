using Microsoft.EntityFrameworkCore;
using ClothingStore.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ClothingStore.Core.Domain.IdentityEntities;

namespace ClothingStore.Infrastructure.DBContext
{
	public class ShopContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		public virtual DbSet<Clothing> Clothes { get; set; }
		public virtual DbSet<ClothingVariant> ClothingVariants { get; set; }
		public virtual DbSet<Customer> Customers { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderDetail> OrderDetails { get; set; }
		public virtual DbSet<Review> Reviews { get; set; }

		public ShopContext(DbContextOptions options) : base(options)
		{
			
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Clothing>().ToTable("Clothes");
			modelBuilder.Entity<ClothingVariant>().ToTable("ClothingVariants");
			modelBuilder.Entity<Customer>().ToTable("Customers");
			modelBuilder.Entity<Order>().ToTable("Orders");
			modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails");
			modelBuilder.Entity<Review>().ToTable("Reviews");
		}
	}
}
