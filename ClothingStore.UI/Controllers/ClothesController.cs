using Microsoft.AspNetCore.Mvc;
using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.DTO.Clothing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.DTO.ClothingVariants;

namespace ClothingStore.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class ClothesController : ControllerBase
	{
		private readonly IClothesService _clothesService;
		private readonly IClothingVariantsService _clothingVariantsService;
		public ClothesController(IClothesService clothesService, IClothingVariantsService clothingVariantsService)
		{
			_clothesService = clothesService;
			_clothingVariantsService = clothingVariantsService;
		}
		#region clothes
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
		#endregion
		#region variants
		[HttpGet("{guid}/variant")]
		public async Task<ActionResult<IEnumerable<ClothingVariantResponse>>> GetClothingVariants(Guid guid)
		{
			var clothingVariants = await _clothingVariantsService.GetClothingVariantsByClothingId(guid);
			if(clothingVariants == null)
			{
				return NotFound();
			}
			return clothingVariants;
		}
		[HttpGet("variant/{guid}")]
		public async Task<ActionResult<ClothingVariantResponse>> GetClothingVariant(Guid guid)
		{
			var clothingVariant = await _clothingVariantsService.GetClothingVariantById(guid);
			if (clothingVariant == null)
			{
				return NotFound();
			}
			return clothingVariant;
		}
		[HttpPost("variant")]
		public async Task<ActionResult<ClothingVariantResponse>> PostClothingVariant(
			ClothingVariantAddRequest clothingVariantAddRequest)
		{
			try
			{
				var clothingVar = await _clothingVariantsService.AddClothingVariant(clothingVariantAddRequest);
				return Ok(clothingVar);
				//return CreatedAtAction(nameof(GetClothingVariants), new { guid = clothingVar.ClothingName }, clothingVar);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("variant/{guid}")]
		public async Task<ActionResult<ClothingVariantResponse>> PutClothingVariant(Guid guid,
			[FromBody]ClothingVariantUpdateRequest clothingVariantUpdateRequest)
		{
			var clothingVar = await _clothingVariantsService.GetClothingVariantById(clothingVariantUpdateRequest.Id);
			if (clothingVar == null)
			{
				return NotFound();
			}
			if (clothingVar.Id != guid)
			{
				return BadRequest();
			}
			var updated = await _clothingVariantsService.UpdateClothingVariant(clothingVariantUpdateRequest);
			return Ok(updated);
		}
		[HttpDelete("variant/{guid}")]
		public async Task<ActionResult<ClothingResponse>> DeleteClothingVariant(Guid guid)
		{
			if (await _clothingVariantsService.DeleteClothingVariantById(guid))
			{
				return NoContent();
			}
			return NotFound();
		}
		#endregion
	}
}
