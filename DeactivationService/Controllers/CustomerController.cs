using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeactivationService.Models;
using DeactivationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DeactivationService.Controllers
{
	[Produces("application/json")]
	[Route("api/customer/[action]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		CustomerService customerService;

		public CustomerController(IConfiguration configuration)
		{
			customerService = new CustomerService(configuration);
		}

		[HttpGet]
		[ActionName("getCustomerList")]
		public IActionResult GetCustomerList(int page, int pageSize, string query)
		{
			if (query == null)
			{
				query = "";
			}
			List<Customer> customers = customerService.GetCustomerList(page, pageSize, query);
			return new ObjectResult(customers);
		}

		[HttpGet]
		[ActionName("getCustomer")]
		public IActionResult GetCustomer(int companyId)
		{
			Customer customer = customerService.GetCustomer(companyId);
			return new ObjectResult(customer);
		}

		[HttpGet]
		[ActionName("getCustomerInfo")]
		public IActionResult GetCustomerInfo(int companyId)
		{
			string authToken = Request.Headers["token"];
			CustomerInfo customer = customerService.GetCustomerInfo(companyId);
			return new ObjectResult(customer);
		}
	}
}