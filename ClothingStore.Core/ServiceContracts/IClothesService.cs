using ClothingStore.Core.DTO.Clothing;

namespace ClothingStore.Core.ServiceContracts
{
    public interface IClothesService
    {
        public Task<ClothingResponse> AddClothing(ClothingAddRequest? clothingAddRequest);
        public Task<List<ClothingResponse>> GetFilteredClothes(FilterClothingDTO? filter);
        public Task<ClothingResponse?> GetClothingById(Guid clothingGuid);
        public Task<ClothingResponse?> GetClothingByName(string clothingName);
        public Task<ClothingResponse> UpdateClothing(ClothingUpdateRequest? clothingUpdateRequest);
        public Task<bool> DeleteClothingById(Guid clothingGuid);

	}
}