using ClothingStore.Core.Domain.IdentityEntities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.Services;
using ClothingStore.Infrastructure.DBContext;
using ClothingStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.UI.Extensions
{
	public static class ConfigureServicesExtension
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection service, IConfiguration configuration)
		{
			service.AddControllersWithViews();

			service.AddScoped<IClothesRepository, ClothesRepository>();
			service.AddScoped<IClothingVariantsRepository, ClothingVariantsRepository>();
			service.AddScoped<IOrdersRepository, OrdersRepository>();
			service.AddScoped<ICustomerRepository, CustomerRepository>();

			service.AddScoped<IClothesService, ClothesService>();
			service.AddScoped<IClothingVariantsService, ClothingVariantsService>();
			service.AddScoped<ICustomerService, CustomerService>();
			service.AddScoped<IOrdersService, OrderService>();

			service.AddDbContext<ShopContext>(options => options.UseSqlServer(
				configuration.GetConnectionString("DefaultConnection")));

			service.AddIdentity<ApplicationUser, ApplicationRole>()
				.AddEntityFrameworkStores<ShopContext>()
				.AddDefaultTokenProviders()
				.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ShopContext, Guid>>()
				.AddRoleStore<RoleStore<ApplicationRole, ShopContext, Guid>>();
			service.AddAuthorization(options =>
			{
				options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
			});
			service.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/"; //TODO
			});

			return service;
		}
	}
}
