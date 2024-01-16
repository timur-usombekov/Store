using ClothingStore.Core.Domain.Entities;

namespace ClothingStore.Core.Domain.RepositoryContracts
{
	public interface IOrderDetailsRepository
	{
		public Task<OrderDetail> AddOrderDetail(OrderDetail clothingVariant);
		public Task<List<OrderDetail>> GetAllOrderDetails();
		public Task<List<OrderDetail>> GetAllOrderDetailsWithNavigationProperties();
		public Task<OrderDetail?> GetOrderDetailById(Guid id);
		public Task<bool> DeleteOrderDetailById(Guid id);
		public Task<OrderDetail> UpdateOrderDetail(OrderDetail clothingVariant);
	}
}
