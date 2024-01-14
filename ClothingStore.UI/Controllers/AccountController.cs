using ClothingStore.Core.Domain.IdentityEntities;
using ClothingStore.Core.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.UI.Controllers
{
	[AllowAnonymous]
	[Route("[controller]/[action]")]
    public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterDTO registerDTO)
		{
			if (!ModelState.IsValid)
			{
				string errors = string.Join("\n", ModelState.Values
					.SelectMany(value => value.Errors)
					.Select(err => err.ErrorMessage));
				return BadRequest(errors);
			}

			ApplicationUser user = new ApplicationUser()
			{
				Email = registerDTO.Email,
				UserName = registerDTO.UserName,
				PhoneNumber = registerDTO.Phone
			};

			IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, isPersistent: true);
				return Ok("User was successfully added");
			}
			else
			{
				return StatusCode(500);
			}
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginDTO loginDTO)
		{
			if (!ModelState.IsValid)
			{
				string errors = string.Join("\n", ModelState.Values
					.SelectMany(value => value.Errors)
					.Select(err => err.ErrorMessage));
				return BadRequest(errors);
			}

			var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, 
				isPersistent: true, lockoutOnFailure: false);

			if (result.Succeeded)
			{
				return Ok("you was successfully sing in");
			}
			else
			{
				return StatusCode(500);
			}
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok();
		}
	}
}
