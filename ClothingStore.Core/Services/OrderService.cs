using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.DTO;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.Helpers;
using ClothingStore.Core.DTO.Orders;
using ClothingStore.Core.DTO.Clothing;
using ClothingStore.Core.Helpers.Extensions;

namespace ClothingStore.Core.Services
{
	public class OrderService : IOrdersService
	{
		private readonly IOrdersRepository _ordersRepository;
		private readonly ICustomerRepository _customerRepository;

		public OrderService(IOrdersRepository ordersRepository, ICustomerRepository customerRepository)
		{
			_ordersRepository = ordersRepository;
			_customerRepository = customerRepository;
		}
		#region Orders Operations
		public async Task<OrderResponse> AddOrder(AddOrderRequest? addOrderRequest)
		{
			if (addOrderRequest is null)
			{
				throw new ArgumentNullException(nameof(addOrderRequest));
			}
			ValidationHelper.ValidateModel(addOrderRequest);
			var customer = await _customerRepository.GetCustomerById(addOrderRequest.CustomerId);
			if (customer == null)
			{
				throw new ArgumentException("This customer does not exist");
			}

			Order order = new Order()
			{
				Id = Guid.NewGuid(),
				CustomerId = addOrderRequest.CustomerId,
				OrderDate = DateTime.Now,
			};
			var addedOrder = await _ordersRepository.AddOrder(order);
			return addedOrder.ToOrderResponse();
		}

		public Task<bool> DeleteOrderById(Guid orderGuid)
		{
			throw new NotImplementedException();
		}

		public Task<OrderResponse> GetAllOrders()
		{
			throw new NotImplementedException();
		}

		public Task<OrderResponse?> GetCustomerOrders(Guid customerGuid)
		{
			throw new NotImplementedException();
		}

		public Task<OrderResponse?> GetOrderById(Guid orderGuid)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
