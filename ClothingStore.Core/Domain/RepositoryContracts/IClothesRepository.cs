using ClothingStore.Core.Domain.Entities;

namespace ClothingStore.Core.Domain.RepositoryContracts
{
	public interface IClothesRepository
	{
		public Task<Clothing> AddClothing(Clothing clothing);
		public Task<List<Clothing>> GetAllClothes();
		public Task<List<Clothing>> GetAllClothesWithNavigationProperties();
		public Task<Clothing?> GetClothingByName(string name);
		public Task<Clothing?> GetClothingById(Guid id);
		public Task<bool> DeleteClothingById(Guid id);
		public Task<Clothing> UpdateClothing(Clothing clothing);
	}
}