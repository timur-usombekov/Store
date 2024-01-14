using ClothingStore.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Core.Domain.RepositoryContracts
{
	public interface IClothingVariantsRepository
	{
		public Task<ClothingVariant> AddClothingVariant(ClothingVariant clothingVariant);
		public Task<List<ClothingVariant>> GetAllClothingVariants();
		public Task<List<ClothingVariant>> GetAllClothingVariantsWithNavigationProperties();
		public Task<ClothingVariant?> GetClothingVariantById(Guid id);
		public Task<bool> DeleteClothingVariantById(Guid id);
		public Task<ClothingVariant> UpdateClothingVariant(ClothingVariant clothingVariant);

	}
}
