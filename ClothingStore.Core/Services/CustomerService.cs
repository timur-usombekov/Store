using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.Domain.RepositoryContracts;
using ClothingStore.Core.DTO.Customer;
using ClothingStore.Core.Domain.Entities;
using ClothingStore.Core.DTO.Orders;
using ClothingStore.Core.Helpers;
using ClothingStore.Core.Helpers.Extensions;

namespace ClothingStore.Core.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public async Task<CustomerResponse> AddCustomer(AddCustomerRequest? addCustomerRequest)
		{
			if (addCustomerRequest is null)
			{
				throw new ArgumentNullException(nameof(addCustomerRequest));
			}
			ValidationHelper.ValidateModel(addCustomerRequest);

			Customer customer = new Customer()
			{
				Id = Guid.NewGuid(),
				Name = addCustomerRequest.Name,
				Email = addCustomerRequest.Email,
			};
			var newCustomer = await _customerRepository.AddCustomer(customer);
			return newCustomer.ToCustomerResponse();
		}

		public async Task<bool> DeleteCustomerById(Guid customerGuid)
		{
			var customer = await _customerRepository.GetCustomerById(customerGuid);
			if(customer == null)
			{
				throw new ArgumentException("Customer does not exist");
			}
			return await _customerRepository.DeleteCustomerById(customerGuid);
		}

		public async Task<List<CustomerResponse>> GetAllCustomers()
		{
			var customers = await _customerRepository.GetAllCustomers();
			return customers.Select(c => c.ToCustomerResponse()).ToList();
		}

		public async Task<CustomerResponse?> GetCustomerById(Guid customerGuid)
		{
			var customer = await _customerRepository.GetCustomerById(customerGuid);
			if (customer == null)
			{
				return null;
			}
			return customer.ToCustomerResponse();
		}

		public async Task<CustomerResponse> UpdateCustomer(UpdateCustomerRequest? updateCustomerRequest)
		{
			if(updateCustomerRequest == null)
			{
				throw new ArgumentNullException(nameof(updateCustomerRequest));
			}
			var customer = await _customerRepository.GetCustomerById(updateCustomerRequest.CustomerId);
			if (customer == null)
			{
				throw new ArgumentException("Customer does not exist");
			}

			customer.Name = updateCustomerRequest.Name ?? customer.Name;
			customer.Email = updateCustomerRequest.Email ?? customer.Email;

			var updatedCust = await _customerRepository.UpdateCustomer(customer);
			return updatedCust.ToCustomerResponse();
		}
	}
}
