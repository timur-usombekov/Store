using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.Helpers;
using ClothingStore.Core.DTO.Orders;
using ClothingStore.Core.Helpers.Extensions;

namespace ClothingStore.Core.Services
{
	public class OrdersService : IOrdersService
	{
		private readonly IOrdersRepository _ordersRepository;
		private readonly ICustomerRepository _customerRepository;

		public OrdersService(IOrdersRepository ordersRepository, ICustomerRepository customerRepository)
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

		public async Task<bool> DeleteOrderById(Guid orderGuid)
		{
			if(await _ordersRepository.GetOrderById(orderGuid) is not null)
			{
				return await _ordersRepository.DeleteOrderById(orderGuid);
			}
			return false;
		}

		public async Task<List<OrderResponse>> GetAllOrders()
		{
			var orders = await _ordersRepository.GetAllOrders();
			var orderResponse = new List<OrderResponse>();
			foreach (var order in orders)
			{
				orderResponse.Add(order.ToOrderResponse());
			}
			return orderResponse;
		}

		public async Task<List<OrderResponse>> GetCustomerOrders(Guid customerGuid)
		{
			if (await _customerRepository.GetCustomerById(customerGuid) is not null)
			{
				var customerOrders = _ordersRepository.GetAllOrders().Result.Where(ord => ord.CustomerId == customerGuid);
				var orderResponse = new List<OrderResponse>();
				foreach (var order in customerOrders)
				{
					orderResponse.Add(order.ToOrderResponse());
				}
				return orderResponse;
			}
			throw new ArgumentException("Customer does not exist");
		}

		public async Task<OrderResponse?> GetOrderById(Guid orderGuid)
		{
			var order = await _ordersRepository.GetOrderById(orderGuid);
			return order?.ToOrderResponse() ?? null;
		}
		#endregion
	}
}
