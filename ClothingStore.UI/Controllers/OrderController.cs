using Microsoft.AspNetCore.Mvc;
using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.DTO.Orders;
using ClothingStore.Core.DTO.Clothing;
using ClothingStore.Core.Services;
using ClothingStore.Core.DTO.OrderDetail;
using Microsoft.AspNetCore.Authorization;

namespace ClothingStore.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class OrderController : ControllerBase
	{
		private readonly IOrdersService _orderService;
		private readonly IOrderDetailsService _orderDetailsService;

		public OrderController(IOrdersService ordersService, IOrderDetailsService orderDetailsService)
		{
			_orderService = ordersService;
			_orderDetailsService = orderDetailsService;
		}
		#region orders
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
		{
			var orders = await _orderService.GetAllOrders();
			return orders;
		}
		[HttpGet("{guid}")]
		public async Task<ActionResult<OrderResponse>> GetOrderById(Guid guid)
		{
			var order = await _orderService.GetOrderById(guid);
			if (order == null)
			{
				return NotFound();
			}
			return order;
		}
		[HttpPost]
		public async Task<ActionResult<OrderResponse>> PostOrder(AddOrderRequest addOrderRequest)
		{
			try
			{
				var order = await _orderService.AddOrder(addOrderRequest);
				return CreatedAtAction(nameof(GetOrderById), new { guid = order.Id }, order);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("{guid}")]
		public async Task<IActionResult> DeleteOrder(Guid guid)
		{
			if (await _orderService.DeleteOrderById(guid))
			{
				return NoContent();
			}
			return NotFound();
		}
		#endregion
		#region orderDetails
		[HttpGet("{guid}/details")]
		public async Task<ActionResult<IEnumerable<OrderDetailResponse>>> GetOrderDetails(Guid guid)
		{
			var ordDetails = await _orderDetailsService.GetOrderDetailsForOrder(guid);
			return ordDetails;
		}
		[HttpGet("details/{guid}")]
		public async Task<ActionResult<OrderDetailResponse>> GetOrderDetail(Guid guid)
		{
			var orderDetail = await _orderDetailsService.GetOrderDetailById(guid);
			if (orderDetail == null)
			{
				return NotFound();
			}
			return orderDetail;
		}
		[HttpPost("details")]
		public async Task<ActionResult<OrderDetailResponse>> PostOrderDetail(AddOrderDetailRequest addOrderDetailRequest)
		{
			try
			{
				var orderDetail = await _orderDetailsService.AddOrderDetail(addOrderDetailRequest);
				return CreatedAtAction(nameof(GetOrderDetail), new { guid = orderDetail.Id }, orderDetail);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("details/{guid}")]
		public async Task<ActionResult<OrderDetailResponse>> PutOrderDetail(Guid guid,
			UpdateOrderDetailRequest updateOrderDetailRequest)
		{
			if(guid != updateOrderDetailRequest.OrderDetailId)
			{
				return BadRequest();
			}
			var detail = await _orderDetailsService.GetOrderDetailById(updateOrderDetailRequest.OrderDetailId);
			if(detail == null)
			{
				return NotFound();
			}
			var updatedDetail = await _orderDetailsService.UpdateOrderDetail(updateOrderDetailRequest);
			return Ok(updatedDetail);
		}
		[HttpDelete("details/{guid}")]
		public async Task<IActionResult> DeleteOrderDetail(Guid guid)
		{
			if (await _orderDetailsService.DeleteOrderDetailById(guid))
			{
				return NoContent();
			}
			return NotFound();
		}
		#endregion
	}
}
