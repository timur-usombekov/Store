using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Repositories
{
	public class OrderDetailsRepository : IOrderDetailsRepository
	{
		private readonly ShopContext _dbContext;
		public OrderDetailsRepository(ShopContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<OrderDetail> AddOrderDetail(OrderDetail clothingVariant)
		{
			_dbContext.OrderDetails.Add(clothingVariant);
			await _dbContext.SaveChangesAsync();
			return await GetOrderDetailById(clothingVariant.Id) ?? throw new ArgumentException("OrderDetail was not found");
		}

		public async Task<bool> DeleteOrderDetailById(Guid id)
		{
			var orderDetail = await _dbContext.OrderDetails.FindAsync(id);
			if (orderDetail is not null)
			{
				_dbContext.OrderDetails.Remove(orderDetail);
				await _dbContext.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<List<OrderDetail>> GetAllOrderDetails()
		{
			return await _dbContext.OrderDetails.ToListAsync();
		}

		public async Task<List<OrderDetail>> GetAllOrderDetailsWithNavigationProperties()
		{
			return await _dbContext.OrderDetails
				.Include(od => od.ClothingVariant)
				.ToListAsync();
		}

		public async Task<OrderDetail?> GetOrderDetailById(Guid id)
		{
			return await _dbContext.OrderDetails.Include(od => od.ClothingVariant)
				.FirstOrDefaultAsync(od => od.Id == id);
		}

		public async Task<OrderDetail> UpdateOrderDetail(OrderDetail clothingVariant)
		{
			_dbContext.OrderDetails.Update(clothingVariant);
			await _dbContext.SaveChangesAsync();
			return await GetOrderDetailById(clothingVariant.Id) ?? throw new ArgumentException("OrderDetail was not found");
		}
	}
}
