using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Infrastructure.Repositories
{
	public class ClothingVariantsRepository: IClothingVariantsRepository
	{
		private readonly ShopContext _dbContext;

		public ClothingVariantsRepository(ShopContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<ClothingVariant> AddClothingVariant(ClothingVariant clothingVariant)
		{
			_dbContext.ClothingVariants.Add(clothingVariant);
			await _dbContext.SaveChangesAsync();

			ClothingVariant addedClothingVariant = await GetClothingVariantById(clothingVariant.Id) ?? throw new ArgumentNullException("Clothing variant was not founded");
			return addedClothingVariant;
		}
		public async Task<bool> DeleteClothingVariantById(Guid id)
		{
			var clothingVariant = await _dbContext.ClothingVariants.FindAsync(id);
			if (clothingVariant == null) { return false; }
			_dbContext.ClothingVariants.Remove(clothingVariant);
			await _dbContext.SaveChangesAsync();
			return true;
		}
		public async Task<List<ClothingVariant>> GetAllClothingVariants()
		{
			return await _dbContext.ClothingVariants.ToListAsync();
		}
		public async Task<List<ClothingVariant>> GetAllClothingVariantsWithNavigationProperties()
		{
			return await _dbContext.ClothingVariants
					.Include(c => c.Clothing)
					.ToListAsync();
		}
		public async Task<ClothingVariant?> GetClothingVariantById(Guid id)
		{
			return await _dbContext.ClothingVariants
				.Include(cv => cv.OrderDetails)
				.Include(cv => cv.Clothing)
				.FirstOrDefaultAsync(cv => cv.Id == id);
		}
		public async Task<ClothingVariant> UpdateClothingVariant(ClothingVariant clothingVariant)
		{
			_dbContext.ClothingVariants.Update(clothingVariant);
			await _dbContext.SaveChangesAsync();
			return clothingVariant;
		}

	}
}
