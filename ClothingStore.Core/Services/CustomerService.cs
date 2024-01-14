using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.DTO;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.Helpers;
using ClothingStore.Core.DTO.Customer;
using ClothingStore.Core.DTO.Orders;

namespace ClothingStore.Core.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public Task<CustomerResponse> AddOrder(AddOrderRequest? addOrderRequest)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteOrderById(Guid orderGuid)
		{
			throw new NotImplementedException();
		}

		public Task<CustomerResponse> GetAllOrders()
		{
			throw new NotImplementedException();
		}

		public Task<CustomerResponse?> GetCustomerOrders(Guid customerGuid)
		{
			throw new NotImplementedException();
		}

		public Task<CustomerResponse?> GetOrderById(Guid orderGuid)
		{
			throw new NotImplementedException();
		}
	}
}
