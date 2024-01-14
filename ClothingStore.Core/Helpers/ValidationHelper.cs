using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Core.Helpers
{
	public class ValidationHelper
	{
		public static void ValidateModel(object obj)
		{
			ValidationContext context = new(obj);
			List<ValidationResult> results = new();

			bool isValid = Validator.TryValidateObject(obj, context, results);

			if (!isValid)
			{
				throw new ArgumentException(results.FirstOrDefault()?.ErrorMessage);
			}
		}
	}
}
