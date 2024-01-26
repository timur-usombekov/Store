using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.DTO.Clothing;
using ClothingStore.Core.DTO.Customer;
using ClothingStore.Core.DTO.ClothingVariants;
using ClothingStore.Core.DTO.OrderDetail;
using ClothingStore.Core.DTO.Orders;

namespace ClothingStore.Core.Helpers.Extensions
{
    public static class Extensions
	{
		public static ClothingResponse ToClothingResponse(this Clothing clothing)
		{
			ClothingResponse response = new ClothingResponse()
			{
				Id = clothing.Id,
				Name = clothing.Name,
				TotalStock = clothing.TotalStock,
				Category = clothing.Category,
				Description = clothing.Description,
				Brand = clothing.Brand,
				Price = clothing.Price,
				Rating = clothing.Rating,
				clothingVariants = clothing.ClothingVariants.Select(c => c.ToClothingVariantResponse()).ToList()
			};

			return response;
		}
		public static ClothingVariantResponse ToClothingVariantResponse(this ClothingVariant clothingVariant)
		{
			ClothingVariantResponse response = new()
			{
				Id = clothingVariant.Id,
				ClothingName = clothingVariant.Clothing.Name,
				Color = clothingVariant.Color,
				Image = clothingVariant.Image,
				Size = clothingVariant.Size,
				Stock = clothingVariant.Stock
			};

			return response;
		}
		public static CustomerResponse ToCustomerResponse(this Customer customer)
		{
			CustomerResponse response = new()
			{
				Name = customer.Name,
			};

			return response;
		}

		public static OrderResponse ToOrderResponse(this Order order)
		{
			OrderResponse response = new()
			{
				Id = order.Id,
				OrderDate = order.OrderDate,
				Customer = order.Customer.ToCustomerResponse(),
				OrderDetails = new List<OrderDetailResponse>()
			};

			return response;
		}
		public static OrderDetailResponse ToOrderDetailResponse(this OrderDetail orderDetail)
		{
			OrderDetailResponse response = new()
			{
				Quantity = orderDetail.Quantity,
				ClothingVariant = orderDetail.ClothingVariant.ToClothingVariantResponse()
			};

			return response;
		}

		/// <summary>
		/// Checks whether <paramref name="enumerable"/> is null or empty.
		/// </summary>
		/// <typeparam name="T">The type of the <paramref name="enumerable"/>.</typeparam>
		/// <param name="enumerable">The <see cref="IEnumerable{T}"/> to be checked.</param>
		/// <returns>True if <paramref name="enumerable"/> is null or empty, false otherwise.</returns>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable == null || !enumerable.Any();
		}
	}
}
