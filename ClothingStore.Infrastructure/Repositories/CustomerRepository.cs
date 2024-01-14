using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly ShopContext _dbContext;

		public CustomerRepository(ShopContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Customer> AddCustomer(Customer customer)
		{
			_dbContext.Customers.Add(customer);
			await _dbContext.SaveChangesAsync();
			return await GetCustomerById(customer.Id);
		}

		public async Task<bool> DeleteCustomerById(Guid id)
		{
			Customer? customer = await _dbContext.Customers.FindAsync(id);
			if (customer == null) { return false; }
			_dbContext.Customers.Remove(customer);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<List<Customer>> GetAllCustomers()
		{
			return await _dbContext.Customers.ToListAsync();
		}

		public async Task<List<Customer>> GetAllCustomersWithNavigationProperties()
		{
			return await _dbContext.Customers
				.Include(c => c.Orders)
				.ToListAsync();
		}

		public async Task<Customer?> GetCustomerByEmail(string email)
		{
			return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
		}

		public async Task<Customer?> GetCustomerById(Guid id)
		{
			return await _dbContext.Customers
				.Include(c => c.Orders)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Customer> UpdateClothing(Customer customer)
		{
			_dbContext.Customers.Update(customer);
			await _dbContext.SaveChangesAsync();
			return customer;
		}
	}
}
