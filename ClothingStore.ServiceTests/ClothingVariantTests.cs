using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ClothingStore.Core.DTO.ClothingVariants;

namespace ClothingStore.ServiceTests.ClothingVariantsServiceTests
{
    public class ClothingVariantTests
	{
		private readonly IClothingVariantsService _clothingVariantsService; 
		private readonly IClothingVariantsRepository _clothingVariantsRepository;
		private readonly IClothesRepository _clothesRepository;

		public ClothingVariantTests()
		{
			_clothesRepository = Substitute.For<IClothesRepository>();
			_clothingVariantsRepository = Substitute.For<IClothingVariantsRepository>();
			_clothingVariantsService = new ClothingVariantsService(_clothingVariantsRepository, _clothesRepository);
		}
		[Fact]
		public async Task AddClothingVariant_EmptyName_ReturnArgumentExeption()
		{
			var request = new ClothingVariantAddRequest()
			{
				ClothingId = Guid.Empty,
			};

			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await _clothingVariantsService.AddClothingVariant(request);
			});
		}
		[Fact]
		public async Task AddClothingVariant_NameNotExist_ReturnArgumentExeption()
		{
			_clothesRepository.GetClothingByName(Arg.Any<string>()).ReturnsNull();

			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await _clothingVariantsService.AddClothingVariant(new ClothingVariantAddRequest());
			});
		}
		[Fact]
		public async Task AddClothingVariant_CorrectRequest_ReturnResponseWithClothing()
		{
			var clothing = new Clothing() { Id = Guid.NewGuid(), Name = "name" };
			var request = new ClothingVariantAddRequest()
			{
				ClothingId = Guid.NewGuid(),
				Color = "Red",
				Size = Size.XL
			};
			_clothesRepository.GetClothingById(Arg.Any<Guid>()).Returns(clothing);
			_clothingVariantsRepository.AddClothingVariant(Arg.Any<ClothingVariant>()).Returns(new ClothingVariant());

			var response = await _clothingVariantsService.AddClothingVariant(request);
			Assert.True(clothing.Name == response.ClothingName);
		}
		[Fact]
		public async Task GetClothingVariantById_IdNotExist_ReturnsNull()
		{
			_clothingVariantsRepository.GetClothingVariantById(Arg.Any<Guid>()).ReturnsNull();

			Assert.Null(await _clothingVariantsRepository.GetClothingVariantById(Guid.NewGuid()));
		}
		[Fact]
		public async Task GetClothingVariantById_IdExist_ReturnsClothingVariantResponse()
		{
			ClothingVariant clothingVariant = new ClothingVariant() { Id = Guid.NewGuid() };
			_clothingVariantsRepository.GetClothingVariantById(clothingVariant.Id).Returns(clothingVariant);

			var resp = await _clothingVariantsRepository.GetClothingVariantById(clothingVariant.Id);
			Assert.NotNull(resp);
			Assert.True(resp.Id.Equals(clothingVariant.Id));
		}
		[Fact]
		public async Task GetClothingVariantsByClothingName_NameNotExist_ReturnsAgumentExeption()
		{
			_clothingVariantsRepository.GetAllClothingVariantsWithNavigationProperties().Returns(new List<ClothingVariant>());

			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await _clothingVariantsService.GetClothingVariantsByClothingName("some name");
			});
		}
		[Fact]
		public async Task GetClothingVariantsByClothingName_NameExist_ReturnsListWithVariants()
		{
			Clothing firstClothing = new()
			{
				Name = "first",
				Id = Guid.NewGuid(),
			};
			Clothing secondClothing = new()
			{
				Name = "second",
				Id = Guid.NewGuid(),
			};
			var repositoryReturns = new List<ClothingVariant>()
			{
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = firstClothing, ClothingId = firstClothing.Id},
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = firstClothing, ClothingId = firstClothing.Id},
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = firstClothing, ClothingId = firstClothing.Id},
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = secondClothing, ClothingId = secondClothing.Id},
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = secondClothing, ClothingId = secondClothing.Id}
			};
			_clothesRepository.GetClothingByName(secondClothing.Name).Returns(secondClothing);
			_clothingVariantsRepository.GetAllClothingVariantsWithNavigationProperties().Returns(repositoryReturns);

			var resp = await _clothingVariantsService.GetClothingVariantsByClothingName("second");

			var expectedResponse = new List<ClothingVariantResponse>()
			{
				new ClothingVariantResponse { Id = repositoryReturns[3].Id, ClothingName = secondClothing.Name},
				new ClothingVariantResponse { Id = repositoryReturns[4].Id, ClothingName = secondClothing.Name}
			};

			Assert.NotEmpty(resp);
			Assert.Equivalent(expectedResponse, resp);
		}
		[Fact]
		public async Task GetClothingVariantsByClothingId_IdNotExist_ReturnsArgumentExeption()
		{
			_clothesRepository.GetClothingById(Arg.Any<Guid>()).ReturnsNull();
			_clothingVariantsRepository.GetAllClothingVariantsWithNavigationProperties().Returns(new List<ClothingVariant>());

			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await _clothingVariantsService.GetClothingVariantsByClothingId(Guid.NewGuid());
			});
		}
		[Fact]
		public async Task GetClothingVariantsByClothingId_IdExist_ReturnsListWithVariants()
		{
			Clothing firstClothing = new()
			{
				Name = "first",
				Id = Guid.NewGuid(),
			};
			Clothing secondClothing = new()
			{
				Name = "second",
				Id = Guid.NewGuid(),
			};
			var repositoryReturns = new List<ClothingVariant>()
			{
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = firstClothing, ClothingId = firstClothing.Id},
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = firstClothing, ClothingId = firstClothing.Id},
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = firstClothing, ClothingId = firstClothing.Id},
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = secondClothing, ClothingId = secondClothing.Id},
				new ClothingVariant { Id = Guid.NewGuid(), Clothing = secondClothing, ClothingId = secondClothing.Id}
			};
			_clothesRepository.GetClothingById(secondClothing.Id).Returns(secondClothing);
			_clothingVariantsRepository.GetAllClothingVariantsWithNavigationProperties().Returns(repositoryReturns);

			var resp = await _clothingVariantsService.GetClothingVariantsByClothingId(secondClothing.Id);

			var expectedResponse = new List<ClothingVariantResponse>()
			{
				new ClothingVariantResponse { Id = repositoryReturns[3].Id, ClothingName = secondClothing.Name},
				new ClothingVariantResponse { Id = repositoryReturns[4].Id, ClothingName = secondClothing.Name}
			};

			Assert.NotEmpty(resp);
			Assert.Equivalent(expectedResponse, resp);
		}
		[Fact]
		public async Task UpdateClothingVariant_RequestIsNull_ReturnsArgumentNullExeption()
		{
			ClothingVariantUpdateRequest? request = null;

			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				await _clothingVariantsService.UpdateClothingVariant(request);
			});
		}
		[Fact]
		public async Task UpdateClothingVariant_CorrectRequest_ReturnsUpdatedClothingVariant()
		{
			ClothingVariantUpdateRequest request = new() 
			{ 
				Id = Guid.NewGuid(),
				Color = "NewColor",
				Image = "NewImage",
				Size = Size.XL
			};
			Clothing clothing = new() { Name = "No name"};
			_clothingVariantsRepository.GetClothingVariantById(request.Id).Returns(new ClothingVariant()
			{
				Id = request.Id,
				Clothing = clothing,
			});
			ClothingVariantResponse expectedResponse = new()
			{
				Id = request.Id,
				ClothingName = "No name",
				Color = "NewColor",
				Image = "NewImage",
				Size = Size.XL,
			};

			var resp = await _clothingVariantsService.UpdateClothingVariant(request);

			Assert.Equivalent(expectedResponse, resp);
		}
		[Fact]
		public async Task UpdateClothingVariant_ClothingVariantNotFound_ReturnsArgumentExeption()
		{
			ClothingVariantUpdateRequest request = new() { Id = Guid.NewGuid()};

			_clothingVariantsRepository.GetClothingVariantById(request.Id).ReturnsNull();

			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await _clothingVariantsService.UpdateClothingVariant(request);
			});
		}
		[Fact]
		public async Task DeleteClothingVariantById_ClothingVariantNotFound_ReturnsFalse()
		{
			_clothingVariantsRepository.DeleteClothingVariantById(Arg.Any<Guid>()).Returns(false);
			_clothingVariantsRepository.GetClothingVariantById(Arg.Any<Guid>()).ReturnsNull();

			Assert.False(await _clothingVariantsService.DeleteClothingVariantById(Guid.NewGuid()));
		}
		[Fact]
		public async Task DeleteClothingVariantById_ClothingVariantWasFound_ReturnsTrue()
		{
			var id = Guid.NewGuid();
			_clothingVariantsRepository.DeleteClothingVariantById(Arg.Any<Guid>()).Returns(true);
			_clothingVariantsRepository.GetClothingVariantById(Arg.Any<Guid>()).Returns(new ClothingVariant()
			{
				Id = id
			});

			Assert.True(await _clothingVariantsService.DeleteClothingVariantById(id));
		}
	}
}
