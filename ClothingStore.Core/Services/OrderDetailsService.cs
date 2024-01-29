using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.DTO.ClothingVariants;
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
		private readonly IClothingVariantsRepository _clothingVariantsRepository;

		public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository, IOrdersRepository ordersRepository,
			IClothingVariantsRepository clothingVariantsRepository)
		{
			_orderDetailsRepository = orderDetailsRepository;
			_ordersRepository = ordersRepository;
			_clothingVariantsRepository = clothingVariantsRepository;
		}

		public async Task<OrderDetailResponse> AddOrderDetail(AddOrderDetailRequest? addOrderDetailRequest)
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
			var variant = await _clothingVariantsRepository.GetClothingVariantById(addOrderDetailRequest.ClothingVariantId) ??
				throw new ArgumentException("Clothing variant doesn't exist");
			if (addOrderDetailRequest.Quantity > variant.Stock)
			{
				throw new ArgumentException("Insufficient stock for the order");
			}

			OrderDetail orderDetail = new OrderDetail()
			{
				Id = Guid.NewGuid(),
				ClothingVariantId = addOrderDetailRequest.ClothingVariantId,
				OrderID = addOrderDetailRequest.OrderID,
				Quantity = addOrderDetailRequest.Quantity,
			};

			var orderDetailWithNavProp = await _orderDetailsRepository.AddOrderDetail(orderDetail);
			variant.Stock -= orderDetail.Quantity;
			await _clothingVariantsRepository.UpdateClothingVariant(variant);
			orderDetailWithNavProp.ClothingVariant = variant;
			return orderDetailWithNavProp.ToOrderDetailResponse();
		}

		public async Task<bool> DeleteOrderDetailById(Guid orderDetailId)
		{
			var ordDet = await _orderDetailsRepository.GetOrderDetailById(orderDetailId); 
			if(ordDet is null)
			{
				throw new ArgumentException("orderDetail doesn't exist");
			}
			var clothingVariant = await _clothingVariantsRepository.GetClothingVariantById(ordDet.ClothingVariantId);
			if(clothingVariant is null)
			{
				throw new ArgumentException("clothingVariant doesn't exist");
			}
			clothingVariant.Stock += ordDet.Quantity;
			await _clothingVariantsRepository.UpdateClothingVariant(clothingVariant);
			return await _orderDetailsRepository.DeleteOrderDetailById(orderDetailId);
		}

		public async Task<List<OrderDetailResponse>> GetAllOrderDetails()
		{
			var ordDet = await _orderDetailsRepository.GetAllOrderDetailsWithNavigationProperties();
			foreach(var detail in ordDet)
			{
				detail.ClothingVariant =
					await _clothingVariantsRepository.GetClothingVariantById(detail.ClothingVariantId);
			}
			return ordDet.Select(od => od.ToOrderDetailResponse()).ToList();
		}

		public async Task<OrderDetailResponse?> GetOrderDetailById(Guid orderDetailId)
		{
			var ordDet = await _orderDetailsRepository.GetOrderDetailById(orderDetailId);
			if (ordDet is null)
			{
				throw new ArgumentException("orderDetail doesn't exist");
			}
			ordDet.ClothingVariant =
				await _clothingVariantsRepository.GetClothingVariantById(ordDet.ClothingVariantId);
			return ordDet.ToOrderDetailResponse();
		}

		public async Task<List<OrderDetailResponse>> GetOrderDetailsForOrder(Guid orderId)
		{
			if (await _ordersRepository.GetOrderById(orderId) is null)
			{
				throw new ArgumentException("Order doesn't exist");
			}
			var allOrderDetails = await _orderDetailsRepository.GetAllOrderDetailsWithNavigationProperties();
			var orderDetailsForOrder = allOrderDetails.Where(od => od.OrderID == orderId);
			foreach (var orderDetail in orderDetailsForOrder)
			{
				orderDetail.ClothingVariant =
					await _clothingVariantsRepository.GetClothingVariantById(orderDetail.ClothingVariantId);
			}

			return orderDetailsForOrder
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
			var variant = await _clothingVariantsRepository.GetClothingVariantById(oldOrderDetail.ClothingVariantId) ??
				throw new ArgumentException("Clothing variant doesn't exist");
			if (oldOrderDetail.Quantity > variant.Stock)
			{
				throw new ArgumentException("Insufficient stock for the order");
			}
			oldOrderDetail.Id = oldOrderDetail.Id;
			oldOrderDetail.ClothingVariantId = updateOrderDetailRequest.ClothingVariantId ?? oldOrderDetail.ClothingVariantId;
			oldOrderDetail.OrderID = updateOrderDetailRequest.OrderId ?? oldOrderDetail.OrderID;
			oldOrderDetail.Quantity = updateOrderDetailRequest.Quantity ?? oldOrderDetail.Quantity;
			
			var ord = await _orderDetailsRepository.UpdateOrderDetail(oldOrderDetail);
			ord.ClothingVariant = variant;
			return ord.ToOrderDetailResponse();
		}
	}
}
