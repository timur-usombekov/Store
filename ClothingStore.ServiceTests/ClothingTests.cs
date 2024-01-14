using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.DTO;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ClothingStore.Core.DTO.Clothing;

namespace ClothingStore.ServiceTests.ClothesServiceTests
{
    public class ClothingTests
	{
		private readonly IClothesService _clothesService;
		private readonly IClothesRepository _clothesRepository;

		public ClothingTests()
		{
			_clothesRepository = Substitute.For<IClothesRepository>();
			_clothesService = new ClothesService(_clothesRepository);
		}
		[Fact]
		public async Task AddClothing_IncorrectArguments_RetunrnsArgumentException()
		{
			// Arange
			var clothingAddRequest = new ClothingAddRequest()
			{
				Name = null,
				Stock = -1,
				Price = -1
			};

			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				// Act
				await _clothesService.AddClothing(clothingAddRequest);
			});
		}
		[Fact]
		public async Task AddClothing_GUIDGeneration_ReturnsNotEmptyGUID()
		{
			var clothingAddRequest = new ClothingAddRequest()
			{
				Name = "name",
			};
			_clothesRepository.GetClothingByName(Arg.Any<string>()).ReturnsNull();
			_clothesRepository.AddClothing(Arg.Any<Clothing>()).Returns(new Clothing());

			var resp = await _clothesService.AddClothing(clothingAddRequest);
			Assert.True(resp.Id != Guid.Empty);
		}
		[Fact]
		public async Task AddClothing_ExistingName_ReturnsArgumentException()
		{
			var clothingAddRequest = new ClothingAddRequest()
			{
				Name = "name",
			};
			_clothesRepository.GetClothingByName(Arg.Any<string>()).Returns(new Clothing());

			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await _clothesService.AddClothing(clothingAddRequest);
			});
		}
		[Fact]
		public async Task GetClothingById_ClothingNotFound_ReturnsNull()
		{
			_clothesRepository.GetClothingById(Arg.Any<Guid>()).ReturnsNull();

			Assert.Null(await _clothesService.GetClothingById(Guid.NewGuid()));
		}
		[Fact]
		public async Task GetClothingById_ClothingWasFound_ReturnsClothing()
		{
			Clothing clothing = new Clothing() { Id = Guid.NewGuid(), Name = "name" };
			_clothesRepository.GetClothingById(clothing.Id).Returns(clothing);

			var resp = await _clothesService.GetClothingById(clothing.Id);
			Assert.True(resp?.Id.Equals(clothing.Id));
			Assert.True(resp?.Name == clothing.Name); 
		}
		[Fact]
		public async Task GetClothingByName_ClothingNotFound_ReturnsNull()
		{
			_clothesRepository.GetClothingByName(Arg.Any<string>()).ReturnsNull();

			Assert.Null(await _clothesService.GetClothingByName("some string"));
		}
		[Fact]
		public async Task GetClothingByName_ClothingWasFound_ReturnsClothing()
		{
			Clothing clothing = new Clothing() { Id = Guid.NewGuid(), Name = "name" };
			_clothesRepository.GetClothingByName(clothing.Name).Returns(clothing);

			var resp = await _clothesService.GetClothingByName(clothing.Name);
			Assert.True(resp?.Id.Equals(clothing.Id));
			Assert.True(resp?.Name == clothing.Name);
		}
		[Fact]
		public async Task GetFilteredClothes_StringFilter_ReturnCorrectFilteredClothes() 
		{
			var firstClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name", Brand = "Nike" };
			var secondClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name1", Brand = "Nike" };
			var thirdClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name2", Brand = "Adidas" };

			firstClothing.ClothingVariants = new List<ClothingVariant>()
			{
				new ClothingVariant() { ClothingId = firstClothing.Id, Clothing = firstClothing, Color = "Green" }
			};
			secondClothing.ClothingVariants = new List<ClothingVariant>()
			{
				new ClothingVariant() { ClothingId = secondClothing.Id, Clothing = secondClothing, Color = "Blue" }
			};
			thirdClothing.ClothingVariants = new List<ClothingVariant>()
			{
				new ClothingVariant() { ClothingId = thirdClothing.Id, Clothing = thirdClothing, Color = "Blue" }
			};


			_clothesRepository.GetAllClothesWithNavigationProperties().Returns(new List<Clothing>
			{
				firstClothing, secondClothing, thirdClothing
			});
			List<ClothingResponse> filteredClothes = await _clothesService.GetFilteredClothes
				(new FilterClothingDTO()
				{
					Name = "name",
					Brand = "Nike",
					Color = "Blue"
				});

			Assert.Single(filteredClothes);
			Assert.Equal(secondClothing.Id, filteredClothes.First().Id);
		}
		[Fact]
		public async Task GetFilteredClothes_EnumFilter_ReturnCorrectFilteredClothes()
		{
			var firstClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name", Category = Category.Tshirts };
			var secondClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name1", Category = Category.Pants };
			var thirdClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name2", Category = Category.Tshirts };

			firstClothing.ClothingVariants = new List<ClothingVariant>()
			{
				new ClothingVariant() { ClothingId = firstClothing.Id, Clothing = firstClothing, Size = Size.L }
			};
			secondClothing.ClothingVariants = new List<ClothingVariant>()
			{
				new ClothingVariant() { ClothingId = secondClothing.Id, Clothing = secondClothing, Size = Size.XL }
			};
			thirdClothing.ClothingVariants = new List<ClothingVariant>()
			{
				new ClothingVariant() { ClothingId = thirdClothing.Id, Clothing = thirdClothing, Size = Size.XL }
			};

			_clothesRepository.GetAllClothesWithNavigationProperties().Returns(new List<Clothing>
			{
				firstClothing, secondClothing, thirdClothing
			});

			List<ClothingResponse> filteredClothes = await _clothesService.GetFilteredClothes
				(new FilterClothingDTO()
				{
					Category = Category.Tshirts,
					Size = Size.XL
				});


			Assert.Single(filteredClothes);
			Assert.Equal(thirdClothing.Id, filteredClothes.First().Id);
		}
		[Fact]
		public async Task GetFilteredClothes_CostFilter_ReturnCorrectFilteredClothes()
		{
			var firstClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name", Price = 100 };
			var secondClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name1", Price = 150 };
			var thirdClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name2", Price = 200 };
			var fourthClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name3", Price = 250 };

			_clothesRepository.GetAllClothesWithNavigationProperties().Returns(new List<Clothing>
			{
				firstClothing, secondClothing, thirdClothing, fourthClothing
			});
			List<ClothingResponse> filteredClothes = await _clothesService.GetFilteredClothes
				(new FilterClothingDTO()
				{
					MinimalCost = 125,
					MaximumCost = 200
				});

			Assert.True(filteredClothes.Count == 2);
			Assert.True(secondClothing.Id.Equals(filteredClothes.FirstOrDefault(cl => cl.Id == secondClothing.Id)?.Id));
			Assert.True(thirdClothing.Id.Equals(filteredClothes.FirstOrDefault(cl => cl.Id == thirdClothing.Id)?.Id));
		}
		[Fact]
		public async Task GetFilteredClothes_RatingFilter_ReturnCorrectFilteredClothes()
		{
			var firstClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name", Rating = 3 };
			var secondClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name1", Rating = 3.4 };
			var thirdClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name2", Rating = 4.2 };
			var fourthClothing = new Clothing() { Id = Guid.NewGuid(), Name = "name3", Rating = 5 };

			_clothesRepository.GetAllClothesWithNavigationProperties().Returns(new List<Clothing>
			{
				firstClothing, secondClothing, thirdClothing, fourthClothing
			});

			List<ClothingResponse> filteredClothes = await _clothesService.GetFilteredClothes
				(new FilterClothingDTO()
				{
					Rating = 3.5
				});

			Assert.True(filteredClothes.Count == 2);
			Assert.True(thirdClothing.Id.Equals(filteredClothes.FirstOrDefault(cl => cl.Id == thirdClothing.Id)?.Id));
			Assert.True(fourthClothing.Id.Equals(filteredClothes.FirstOrDefault(cl => cl.Id == fourthClothing.Id)?.Id));
		}
		[Fact]
		public async Task DeleteClothingById_ClothingNotFound_FalseAsResult()
		{
			_clothesRepository.GetClothingById(Arg.Any<Guid>()).ReturnsNull();
			Assert.False(await _clothesService.DeleteClothingById(Guid.NewGuid()));
		}
		[Fact]
		public async Task DeleteClothingById_ClothingWasFound_TrueAsResult()
		{
			_clothesRepository.GetClothingById(Arg.Any<Guid>()).Returns(new Clothing());
			_clothesRepository.DeleteClothingById(Arg.Any<Guid>()).Returns(true);

			Assert.True(await _clothesService.DeleteClothingById(Guid.NewGuid()));
		}
		[Fact] 
		public async Task UpdateClothing_UpdateRequestIsNull_ReturnArgumentNullExeption()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				await _clothesService.UpdateClothing(null);
			});
		}
		[Fact]
		public async Task UpdateClothing_ClothingIdNotFound_ReturnArgumentExeption()
		{
			_clothesRepository.GetClothingById(Arg.Any<Guid>()).ReturnsNull();

			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				await _clothesService.UpdateClothing(new ClothingUpdateRequest() { Id = Guid.NewGuid()});
			});
		}
		[Fact]
		public async Task UpdateClothing_CorrectRequest_UpdatedClothing()
		{
			var oldClothing = new Clothing()
			{
				Id = Guid.NewGuid(),
				Name = "OldName",
				Description = "OldDescription",
				Brand = "OldBrand",
				Category = Category.Tshirts,
				Price = 10,
				Stock = 10,
			};
			_clothesRepository.GetClothingById(oldClothing.Id).Returns(oldClothing);

			var newClothingRequest = new ClothingUpdateRequest()
			{
				Id = oldClothing.Id,
				Name = "NewName",
				Description = "NewDescription",
				Brand = "NewBrand",
				Category = Category.Dresses,
				Price = 100,
				Stock = 100,
			};

			var newClothingResponse = await _clothesService.UpdateClothing(newClothingRequest);

			Assert.Equivalent(newClothingRequest, newClothingResponse);
		}
	}
}