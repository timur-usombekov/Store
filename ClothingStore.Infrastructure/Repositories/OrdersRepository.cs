using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Repositories
{
	public class OrdersRepository : IOrdersRepository
	{
		private readonly ShopContext _dbContext;
		public OrdersRepository(ShopContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<Order> AddOrder(Order order)
		{
			_dbContext.Orders.Add(order);
			await _dbContext.SaveChangesAsync();
			return order;
		}

		public async Task<bool> DeleteOrderById(Guid id)
		{
			Order? order = await _dbContext.Orders.FindAsync(id);
			if (order == null) { return false; }
			_dbContext.Orders.Remove(order);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<List<Order>> GetAllOrders()
		{
			return await _dbContext.Orders.ToListAsync();
		}

		public async Task<List<Order>> GetAllOrdersWithNavigationProperties()
		{
			return await _dbContext.Orders
				.Include(o => o.OrderDetails)
				.Include(o => o.Customer)
				.ToListAsync();
		}

		public async Task<Order?> GetOrderById(Guid id)
		{
			return await _dbContext.Orders
				.Include (o => o.OrderDetails)
				.Include (o => o.Customer)
				.FirstOrDefaultAsync(o => o.Id == id);
		}
	}
}
