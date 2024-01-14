using Microsoft.AspNetCore.Mvc;
using ClothingStore.Core.ServiceContracts;

namespace ClothingStore.UI.Controllers
{ 
	public class ClothesController : Controller
	{
		private readonly IClothesService _clothesService;
		
		public ClothesController(IClothesService clothesService)
		{
			_clothesService = clothesService;
		}

		[Route("/clothes")]
		public async Task<IActionResult> Clothes()
		{
			return Ok();
		}
	}
}
