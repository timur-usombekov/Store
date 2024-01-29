using ClothingStore.Core.DTO.Clothing;
using ClothingStore.Core.DTO.Customer;
using ClothingStore.Core.DTO.Orders;

namespace ClothingStore.Core.ServiceContracts
{
    public interface ICustomerService
	{
		public Task<CustomerResponse> AddCustomer(AddCustomerRequest? addCustomerRequest);
		public Task<List<CustomerResponse>> GetAllCustomers();
		public Task<CustomerResponse?> GetCustomerById(Guid customerGuid);
		public Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest? updateCustomerRequest);
		public Task<bool> DeleteCustomerById(Guid customerGuid);
	}
}
