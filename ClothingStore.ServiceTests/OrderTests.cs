using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.DTO.Customer;
using ClothingStore.Core.DTO.OrderDetail;
using ClothingStore.Core.DTO.Orders;
using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace ClothingStore.ServiceTests.OrdersServiceTests
{
	public class OrderTests
	{
		private readonly IOrdersService _orderService;
		private readonly IOrdersRepository _ordersRepository;
		private readonly ICustomerRepository _customerRepository;

		public OrderTests()
		{
			_ordersRepository = Substitute.For<IOrdersRepository>();
			_customerRepository = Substitute.For<ICustomerRepository>();
			_orderService = new OrderService(_ordersRepository, _customerRepository);
		}

		[Fact]
		public async Task AddOrder_CustomerNotFounded_RetunrnsArgumentException()
		{
			// Arange
			var orderAddRequest = new AddOrderRequest()
			{
				CustomerId = Guid.NewGuid(),
			};

			_customerRepository.GetCustomerById(Arg.Any<Guid>()).ReturnsNull();

			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				// Act
				await _orderService.AddOrder(orderAddRequest);
			});
		}
		[Fact]
		public async Task AddOrder_CustomerFounded_RetunrnsOrderResponse()
		{
			var customer = new Customer() { Id = Guid.NewGuid(), Name = "Test name" };
			var order = new Order() { Id = Guid.NewGuid(), OrderDate = DateTime.Now,
			OrderDetails = new List<OrderDetail>(), CustomerId = customer.Id, Customer = customer};
			var orderAddRequest = new AddOrderRequest()
			{
				CustomerId = customer.Id
			};

			_customerRepository.GetCustomerById(Arg.Any<Guid>()).Returns(customer);
			_ordersRepository.AddOrder(Arg.Any<Order>()).Returns(order);

			var expectedOrderRespose = new OrderResponse()
			{
				OrderDate = DateTime.Now,
				OrderDetails = new List<OrderDetailResponse>(),
				Customer = new CustomerResponse() { Name = customer.Name },
			};

			var resp = await _orderService.AddOrder(orderAddRequest);
			Assert.NotNull(resp);
			Assert.Empty(resp.OrderDetails);
			Assert.True(resp.Customer.Name == expectedOrderRespose.Customer.Name);
		}
	}
}
