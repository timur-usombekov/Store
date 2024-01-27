using ClothingStore.Core.DTO.Clothing;
using ClothingStore.Core.DTO.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Core.ServiceContracts
{
	public interface IOrderDetailsService
	{
		public Task<OrderDetailResponse> AddOrderDetail(AddOrderDetailRequest? addOrderDetailRequest);
		public Task<List<OrderDetailResponse>> GetAllOrderDetails();
		public Task<OrderDetailResponse?> GetOrderDetailById(Guid orderDetailId);
		public Task<List<OrderDetailResponse>> GetOrderDetailsForOrder(Guid orderId);
		public Task<OrderDetailResponse> UpdateOrderDetail(UpdateOrderDetailRequest? updateOrderDetailRequest);
		public Task<bool> DeleteOrderDetailById(Guid orderDetailId);

	}
}
