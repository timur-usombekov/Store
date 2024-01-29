using ClothingStore.Core.Domain.Entities;

namespace ClothingStore.Core.Domain.RepositoryContracts
{
	public interface ICustomerRepository
	{
		public Task<Customer> AddCustomer(Customer customer);
		public Task<List<Customer>> GetAllCustomers();
		public Task<List<Customer>> GetAllCustomersWithNavigationProperties();
		public Task<Customer?> GetCustomerByEmail(string email);
		public Task<Customer?> GetCustomerById(Guid id);
		public Task<bool> DeleteCustomerById(Guid id);
		public Task<Customer> UpdateCustomer(Customer customer);
	}
}
