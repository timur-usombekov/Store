using ClothingStore.Core.DTO.Customer;
using ClothingStore.Core.DTO.Orders;

namespace ClothingStore.Core.ServiceContracts
{
    public interface ICustomerService
	{
		public Task<CustomerResponse> GetAllOrders();
		public Task<CustomerResponse> AddOrder(AddOrderRequest? addOrderRequest);
		public Task<CustomerResponse?> GetOrderById(Guid orderGuid);
		public Task<CustomerResponse?> GetCustomerOrders(Guid customerGuid);
		public Task<bool> DeleteOrderById(Guid orderGuid);
	}
}
