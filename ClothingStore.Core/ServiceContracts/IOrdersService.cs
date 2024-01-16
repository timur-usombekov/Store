using ClothingStore.Core.DTO.Orders;

namespace ClothingStore.Core.ServiceContracts
{
	public interface IOrdersService 
	{
		public Task<List<OrderResponse>> GetAllOrders();
		public Task<OrderResponse> AddOrder(AddOrderRequest? addOrderRequest);
		public Task<OrderResponse?> GetOrderById(Guid orderGuid);
		public Task<List<OrderResponse>> GetCustomerOrders(Guid customerGuid);
		public Task<bool> DeleteOrderById(Guid orderGuid);
	}
}
