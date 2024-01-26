using Microsoft.AspNetCore.Mvc;
using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.DTO.Clothing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using ClothingStore.Core.Domain.Entities;

namespace ClothingStore.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class ClothesController : ControllerBase
	{
		private readonly IClothesService _clothesService;
		
		public ClothesController(IClothesService clothesService)
		{
			_clothesService = clothesService;
		}

		[HttpGet]
		public async Task<IEnumerable<ClothingResponse>> GetAllClothes()
		{
			var clothesList = await _clothesService.GetFilteredClothes(new FilterClothingDTO());
			return clothesList;
		}
		[HttpGet("{guid}")]
		public async Task<ActionResult<ClothingResponse>> GetClothingByID(Guid guid)
		{
			var obj = await _clothesService.GetClothingById(guid);
			if(obj == null)
			{
				return NotFound();
			}
			return obj;
		}
		[HttpPost]
		public async Task<ActionResult<ClothingResponse>> PostClothing(ClothingAddRequest clothingAddRequest)
		{
			try
			{
				var clothing = await _clothesService.AddClothing(clothingAddRequest);
				return CreatedAtAction(nameof(GetClothingByID), new { guid = clothing.Id }, clothing);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("{guid}")]
		public async Task<ActionResult<ClothingResponse>> PutClothing(Guid guid,
			[FromBody]ClothingUpdateRequest clothingUpdateRequest)
		{
			var clothing = await _clothesService.GetClothingById(clothingUpdateRequest.Id);
			if(clothing == null)
			{
				return NotFound();
			}
			if (clothingUpdateRequest.Id != guid)
			{
				return BadRequest();
			}
			var updated = await _clothesService.UpdateClothing(clothingUpdateRequest);
			return Ok(updated);
		}
		[HttpDelete("{guid}")]
		public async Task<ActionResult<ClothingResponse>> DeleteClothing(Guid guid)
		{
			if (await _clothesService.DeleteClothingById(guid))
			{
				return NoContent();
			}
			return NotFound();
		}
	}
}
