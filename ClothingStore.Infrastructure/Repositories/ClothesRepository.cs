using Microsoft.EntityFrameworkCore;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Infrastructure.DBContext;

namespace ClothingStore.Infrastructure.Repositories
{
	public class ClothesRepository : IClothesRepository
	{
		private readonly ShopContext _dbContext;
		public ClothesRepository(ShopContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Clothing> AddClothing(Clothing clothing)
		{
			_dbContext.Add(clothing);
			await _dbContext.SaveChangesAsync();
			return clothing;
		}
		public async Task<bool> DeleteClothingById(Guid id)
		{
			Clothing? clothing = await _dbContext.Clothes.FindAsync(id);
			if (clothing == null) { return false; }
			_dbContext.Clothes.Remove(clothing);
			await _dbContext.SaveChangesAsync();
			return true;
		}
		public async Task<List<Clothing>> GetAllClothes()
		{
			return await _dbContext.Clothes.ToListAsync();
		}
		public async Task<List<Clothing>> GetAllClothesWithNavigationProperties()
		{
			return await _dbContext.Clothes
					.Include(c => c.ClothingVariants)
					.Include(c => c.Reviews)
					.ToListAsync();
		}
		public async Task<Clothing?> GetClothingById(Guid id)
		{
			return await _dbContext.Clothes.FirstOrDefaultAsync(c => c.Id == id);
		}
		public async Task<Clothing?> GetClothingByName(string name)
		{
			return await _dbContext.Clothes.FirstOrDefaultAsync(c => c.Name == name);
		}
		public async Task<Clothing> UpdateClothing(Clothing clothing)
		{
			_dbContext.Clothes.Update(clothing);
			await _dbContext.SaveChangesAsync();
			return clothing;
		}
	}
}