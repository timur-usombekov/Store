using ClothingStore.Core.Domain.Entities;

namespace ClothingStore.Core.Domain.RepositoryContracts
{
	public interface IOrdersRepository
	{
		public Task<Order> AddOrder(Order order);
		public Task<List<Order>> GetAllOrders();
		public Task<List<Order>> GetAllOrdersWithNavigationProperties();
		public Task<Order?> GetOrderById(Guid id);
		public Task<bool> DeleteOrderById(Guid id);
	}
}
