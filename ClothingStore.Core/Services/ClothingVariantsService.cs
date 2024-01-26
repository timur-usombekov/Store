using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.Helpers;
using ClothingStore.Core.Helpers.Extensions;
using ClothingStore.Core.DTO.ClothingVariants;

namespace ClothingStore.Core.Services
{
    public class ClothingVariantsService : IClothingVariantsService
	{
		private readonly IClothingVariantsRepository _clothingVariantsRepository;
		private readonly IClothesRepository _clothesRepository;

		public ClothingVariantsService(IClothingVariantsRepository clothingVariantsRepository,
			IClothesRepository clothesRepository)
		{
			_clothingVariantsRepository = clothingVariantsRepository;
			_clothesRepository = clothesRepository;
		}

		#region Clothing variantions CRUD operations
		public async Task<ClothingVariantResponse> AddClothingVariant(ClothingVariantAddRequest? clothingVariantAddRequest)
		{
			if(clothingVariantAddRequest is null)
			{
				throw new ArgumentNullException(nameof(clothingVariantAddRequest));
			}

			ValidationHelper.ValidateModel(clothingVariantAddRequest);

			Clothing clothing = await _clothesRepository.GetClothingByName(clothingVariantAddRequest.Name!) ??
				throw new ArgumentException("Can not find clothis with this name");

			ClothingVariant clothingVariant = new()
			{
				Id = Guid.NewGuid(),
				Clothing = clothing,
				ClothingId = clothing.Id,
				Color = clothingVariantAddRequest.Color!,
				Image = clothingVariantAddRequest.Image,
				Size = clothingVariantAddRequest.Size,
			};
			await _clothingVariantsRepository.AddClothingVariant(clothingVariant);
			return clothingVariant.ToClothingVariantResponse();
		}
		public async Task<ClothingVariantResponse?> GetClothingVariantById(Guid clothingVariantGuid)
		{
			var clothingVariant = await _clothingVariantsRepository.GetClothingVariantById(clothingVariantGuid);
			if (clothingVariant == null)
			{
				return null;
			}

			return clothingVariant.ToClothingVariantResponse();
		}
		public async Task<List<ClothingVariantResponse>> GetClothingVariantsByClothingName(string? clothingName)
		{
			if (clothingName == null)
			{
				throw new ArgumentNullException(nameof(clothingName));
			}
			var clothing = await _clothesRepository.GetClothingByName(clothingName) ??
				throw new ArgumentException("Clothing was not found");

			var allClothingVariants = await _clothingVariantsRepository.GetAllClothingVariantsWithNavigationProperties();
			List<ClothingVariantResponse> resp = allClothingVariants.Where(cv => cv.ClothingId == clothing.Id)
				.Select(cv => cv.ToClothingVariantResponse()).ToList()!;
			return resp;
		}
		public async Task<List<ClothingVariantResponse>> GetClothingVariantsByClothingId(Guid clothingGuid)
		{
			var clothing = await _clothesRepository.GetClothingById(clothingGuid) ??
				throw new ArgumentException("Clothing was not found");

			var allClothingVariants = await _clothingVariantsRepository.GetAllClothingVariantsWithNavigationProperties();
			List<ClothingVariantResponse> resp = allClothingVariants.Where(cv => cv.ClothingId == clothing.Id)
				.Select(cv => cv.ToClothingVariantResponse()).ToList()!;
			return resp;
		}
		public async Task<ClothingVariantResponse> UpdateClothingVariant(ClothingVariantUpdateRequest? clothingVariantUpdateRequest)
		{
			if(clothingVariantUpdateRequest == null)
			{
				throw new ArgumentNullException("clothingVariantUpdateRequest is null");
			}
			ValidationHelper.ValidateModel(clothingVariantUpdateRequest);
			var oldClothingVariant = await _clothingVariantsRepository.GetClothingVariantById(clothingVariantUpdateRequest.Id);
			if (oldClothingVariant == null)
			{
				throw new ArgumentException("Clothing variant was not found");
			}
			oldClothingVariant.Id = clothingVariantUpdateRequest.Id;
			oldClothingVariant.Color = clothingVariantUpdateRequest.Color ?? oldClothingVariant.Color;
			oldClothingVariant.Image = clothingVariantUpdateRequest.Image ?? oldClothingVariant.Image;
			oldClothingVariant.Size = clothingVariantUpdateRequest.Size ?? oldClothingVariant.Size;

			await _clothingVariantsRepository.UpdateClothingVariant(oldClothingVariant);
			oldClothingVariant.Clothing = oldClothingVariant.Clothing;
			return oldClothingVariant.ToClothingVariantResponse();
		}
		public async Task<bool> DeleteClothingVariantById(Guid clothingVariantId)
		{
			return await _clothingVariantsRepository.DeleteClothingVariantById(clothingVariantId);
		}
		#endregion

	}
}
