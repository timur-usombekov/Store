using Microsoft.AspNetCore.Mvc;
using ClothingStore.Core.ServiceContracts;

namespace ClothingStore.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrdersService _orderService;

		public OrderController(IOrdersService listOfProducts) 
		{
			_orderService = listOfProducts;
		}

		public IActionResult Order()
		{
			return Ok("You are in order action");
		}
	
	}
}
