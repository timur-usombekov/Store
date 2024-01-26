using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.DTO.OrderDetail;
using ClothingStore.Core.Helpers;
using ClothingStore.Core.Helpers.Extensions;
using ClothingStore.Core.ServiceContracts;

namespace ClothingStore.Core.Services
{
	public class OrderDetailsService : IOrderDetailsService
	{
		private readonly IOrderDetailsRepository _orderDetailsRepository;
		private readonly IOrdersRepository _ordersRepository;

		public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository, IOrdersRepository ordersRepository)
		{
			_orderDetailsRepository = orderDetailsRepository;
			_ordersRepository = ordersRepository;
		}

		public async Task<OrderDetailResponse> AddClothing(AddOrderDetailRequest? addOrderDetailRequest)
		{
			if (addOrderDetailRequest == null)
			{
				throw new ArgumentNullException(nameof(addOrderDetailRequest));
			}
			ValidationHelper.ValidateModel(addOrderDetailRequest);

			if (await _ordersRepository.GetOrderById(addOrderDetailRequest.OrderID) is null)
			{
				throw new ArgumentException("Order doesn't exist");
			}

			OrderDetail orderDetail = new OrderDetail()
			{
				Id = Guid.NewGuid(),
				ClothingVariantId = addOrderDetailRequest.ClothingVariantId,
				OrderID = addOrderDetailRequest.OrderID,
				Quantity = addOrderDetailRequest.Quantity,
			};

			await _orderDetailsRepository.AddOrderDetail(orderDetail);
			var orderDetailWithNavProp = await _orderDetailsRepository.GetOrderDetailById(orderDetail.Id);
			return orderDetailWithNavProp.ToOrderDetailResponse();
		}

		public async Task<bool> DeleteOrderDetailById(Guid orderDetailId)
		{
			var ordDet = await _orderDetailsRepository.GetOrderDetailById(orderDetailId); 
			if(ordDet is null)
			{
				throw new ArgumentException("orderDetail doesn't exist");
			}
			return await _orderDetailsRepository.DeleteOrderDetailById(orderDetailId);
		}

		public async Task<List<OrderDetailResponse>> GetAllOrderDetails()
		{
			var ordDet = await _orderDetailsRepository.GetAllOrderDetailsWithNavigationProperties();
			return ordDet.Select(od => od.ToOrderDetailResponse()).ToList();
		}

		public async Task<OrderDetailResponse?> GetOrderDetailById(Guid orderDetailId)
		{
			var ordDet = await _orderDetailsRepository.GetOrderDetailById(orderDetailId);
			if (ordDet is null)
			{
				throw new ArgumentException("orderDetail doesn't exist");
			}
			return ordDet.ToOrderDetailResponse();
		}

		public async Task<List<OrderDetailResponse>> GetOrderDetailsForOrder(Guid orderId)
		{
			if (await _ordersRepository.GetOrderById(orderId) is null)
			{
				throw new ArgumentException("Order doesn't exist");
			}
			var orderdetails = await _orderDetailsRepository.GetAllOrderDetailsWithNavigationProperties();
			return orderdetails.Where(od => od.OrderID == orderId)
				.Select(od => od.ToOrderDetailResponse())
				.ToList();
		}

		public async Task<OrderDetailResponse> UpdateOrderDetail(UpdateOrderDetailRequest? updateOrderDetailRequest)
		{
			if(updateOrderDetailRequest is null)
			{
				throw new ArgumentNullException(nameof(updateOrderDetailRequest));
			}
			ValidationHelper.ValidateModel(updateOrderDetailRequest);
			var oldOrderDetail = await _orderDetailsRepository.GetOrderDetailById(updateOrderDetailRequest.OrderDetailId);
			if(oldOrderDetail is null)
			{
				throw new ArgumentException("OrderDetail id doesn't exist");
			}

			oldOrderDetail.Id = oldOrderDetail.Id;
			oldOrderDetail.ClothingVariantId = updateOrderDetailRequest.ClothingVariantId ?? oldOrderDetail.ClothingVariantId;
			oldOrderDetail.OrderID = updateOrderDetailRequest.OrderId ?? oldOrderDetail.OrderID;
			oldOrderDetail.Quantity = updateOrderDetailRequest.Quantity ?? oldOrderDetail.Quantity;
			
			var ord = await _orderDetailsRepository.UpdateOrderDetail(oldOrderDetail);
			return ord.ToOrderDetailResponse();
		}
	}
}
