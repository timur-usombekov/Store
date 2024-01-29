using ClothingStore.Core.DTO.Clothing;
using ClothingStore.Core.DTO.Customer;
using ClothingStore.Core.ServiceContracts;
using ClothingStore.Core.Services;
using ClothingStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ClothingStore.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAllCustomers()
		{
			var customers = await _customerService.GetAllCustomers();
			return customers;
		}

		[HttpGet("{guid}")]
		public async Task<ActionResult<CustomerResponse>> GetCustomerById(Guid guid)
		{
			var customer = await _customerService.GetCustomerById(guid);
			if(customer == null)
			{
				return NotFound();
			}
			return customer;
		}

		[HttpPost()]
		public async Task<ActionResult<CustomerResponse>> PostCustomer(AddCustomerRequest addCustomerRequest)
		{
			try
			{
				var customer = await _customerService.AddCustomer(addCustomerRequest);
				return CreatedAtAction(nameof(GetCustomerById), new { guid = customer.Id }, customer);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{guid}")]
		public async Task<ActionResult<CustomerResponse>> PutCustomer(Guid guid, [FromBody] UpdateCustomerRequest updateCustomerRequest)
		{
			var customer = await _customerService.GetCustomerById(updateCustomerRequest.CustomerId);
			if (customer == null)
			{
				return NotFound();
			}
			if (updateCustomerRequest.CustomerId != guid)
			{
				return BadRequest();
			}
			var updated = await _customerService.UpdateCustomer(updateCustomerRequest);
			return Ok(updated);
		}

		[HttpDelete("{guid}")]
		public async Task<IActionResult> Delete(Guid guid)
		{
			if (await _customerService.DeleteCustomerById(guid))
			{
				return NoContent();
			}
			return NotFound();
		}
	}
}
