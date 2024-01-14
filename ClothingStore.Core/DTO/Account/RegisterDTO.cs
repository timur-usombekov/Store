using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Core.DTO.Account
{
	public class RegisterDTO
	{
		[Required]
		public string? UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string? ConfirmPassword { get; set; }
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		[Phone]
		[DataType(DataType.PhoneNumber)]
		public string? Phone { get; set; }
	}
}
