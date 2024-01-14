using ClothingStore.Core.DTO.ClothingVariants;

namespace ClothingStore.Core.ServiceContracts
{
    public interface IClothingVariantsService
	{
		public Task<ClothingVariantResponse> AddClothingVariant(ClothingVariantAddRequest? clothingVariantAddRequest);
		public Task<ClothingVariantResponse?> GetClothingVariantById(Guid clothingVariantGuid);
		public Task<List<ClothingVariantResponse>> GetClothingVariantsByClothingName(string clothingName);
		public Task<List<ClothingVariantResponse>> GetClothingVariantsByClothingId(Guid clothingGuid);
		public Task<ClothingVariantResponse> UpdateClothingVariant(ClothingVariantUpdateRequest? clothingVariantUpdateRequest);
		public Task<bool> DeleteClothingVariantById(Guid clothingVariantId);
	}
}
