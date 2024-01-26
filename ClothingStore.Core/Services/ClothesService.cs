using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.Helpers;
using ClothingStore.Core.Helpers.Extensions;
using ClothingStore.Core.DTO.Clothing;

namespace ClothingStore.Core.Services
{
    public class ClothesService : IClothesService
    {
		private const int DefaultRating = 0;

		private readonly IClothesRepository _clothesRepository;
		
		public ClothesService(IClothesRepository clothesRepository) 
        {
			_clothesRepository = clothesRepository;
		}
		#region Clothes CRUD operations
		public async Task<ClothingResponse> AddClothing(ClothingAddRequest? clothingAddRequest)
		{
			if (clothingAddRequest is null)
			{
				throw new ArgumentNullException(nameof(clothingAddRequest));
			}

			ValidationHelper.ValidateModel(clothingAddRequest);

			if (await _clothesRepository.GetClothingByName(clothingAddRequest.Name!) != null)
			{
				throw new ArgumentException("This name alredy exist");
			}
			
			Clothing clothing = new Clothing()
			{
				Id = Guid.NewGuid(),
				Name = clothingAddRequest.Name!,
				Price = clothingAddRequest.Price,
				Stock = clothingAddRequest.Stock,
				Brand = clothingAddRequest.Brand,
				Category = clothingAddRequest.Category,
				Description = clothingAddRequest.Description,
				Rating = DefaultRating
			};
			await _clothesRepository.AddClothing(clothing);
			return clothing.ToClothingResponse();
		}
		public async Task<List<ClothingResponse>> GetFilteredClothes(FilterClothingDTO? filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException(nameof(filter));
			}

			var clothes = await _clothesRepository.GetAllClothesWithNavigationProperties();
			List<ClothingResponse> clothingResponse =
				clothes.Where(clothing =>
				{
					return (filter.Name == null || clothing.Name.Contains(filter.Name)) &&
					(filter.Brand == null || clothing.Brand == filter.Brand) &&
					(filter.Color == null || !clothing.ClothingVariants.Where(cv => cv.Color == filter.Color).IsNullOrEmpty()) &&
					(filter.MinimalCost == null || clothing.Price >= filter.MinimalCost) &&
					(filter.MaximumCost == null || clothing.Price <= filter.MaximumCost) &&
					(filter.Category == null || clothing.Category == filter.Category) &&
					(filter.Size == null || !clothing.ClothingVariants.Where(cv => cv.Size == filter.Size).IsNullOrEmpty()) &&
					(filter.Rating == null || clothing.Rating >= filter.Rating);
				})
				.Select(cl => cl.ToClothingResponse())
				.ToList();

			return clothingResponse;
		}
		public async Task<ClothingResponse?> GetClothingById(Guid clothingGuid)
		{
			var clothing = await _clothesRepository.GetClothingById(clothingGuid);
			if (clothing is null)
			{
				return null;
			}
			return clothing.ToClothingResponse();

		}
		public async Task<ClothingResponse?> GetClothingByName(string clothingName)
		{
			var clothing = await _clothesRepository.GetClothingByName(clothingName);
			if (clothing is null)
			{
				return null;
			}
			return clothing.ToClothingResponse();
		}
		public async Task<ClothingResponse> UpdateClothing(ClothingUpdateRequest? clothingUpdateRequest)
		{
			if (clothingUpdateRequest == null)
			{
				throw new ArgumentNullException();
			}

			var oldClothing = await _clothesRepository.GetClothingById(clothingUpdateRequest.Id) ??
				throw new ArgumentException("Clothing was not found");

			oldClothing.Name = clothingUpdateRequest.Name ?? oldClothing.Name;
			oldClothing.Description = clothingUpdateRequest.Description ?? oldClothing.Description;
			oldClothing.Brand = clothingUpdateRequest.Brand ?? oldClothing.Brand;
			oldClothing.Category = clothingUpdateRequest.Category ?? oldClothing.Category;
			oldClothing.Price = clothingUpdateRequest.Price ?? oldClothing.Price;
			oldClothing.Stock = clothingUpdateRequest.Stock ?? oldClothing.Stock;

			await _clothesRepository.UpdateClothing(oldClothing);
			return oldClothing.ToClothingResponse();
		}
		public async Task<bool> DeleteClothingById(Guid clothingGuid)
		{
			var clothis = await _clothesRepository.GetClothingById(clothingGuid);
			if (clothis is null)
			{
				return false;
			}
			return await _clothesRepository.DeleteClothingById(clothis.Id);
		}
		#endregion
	}
}