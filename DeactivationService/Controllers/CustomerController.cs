using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeactivationService.Models;
using DeactivationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace DeactivationService.Controllers
{
	[Produces("application/json")]
	[Route("api/customer/[action]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		CustomerService customerService;
		SessionService sessionService;

		public CustomerController(IConfiguration configuration, IMemoryCache memoryCache)
		{
			customerService = new CustomerService(configuration);
			sessionService = new SessionService(configuration, memoryCache);
		}

		[HttpGet]
		[ActionName("getCustomerList")]
		public IActionResult GetCustomerList(int page, int pageSize, string query)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken);

			if(sessionState == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			if (query == null)
			{
				query = "";
			}
			List<Company> customers = customerService.GetCustomerList(page, pageSize, query);
			return new ObjectResult(customers);
		}

		[HttpGet]
		[ActionName("getCustomer")]
		public IActionResult GetCustomer(int companyId)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken, companyId);

			if(sessionState == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			Company customer = customerService.GetCustomer(companyId);
			return new ObjectResult(customer);
		}

		[HttpGet]
		[ActionName("getCustomerInfo")]
		public IActionResult GetCustomerInfo(int companyId)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken, companyId);

			if(sessionState != SessionService.SESSION_STATE.MANAGER)
			{
				return StatusCode(401);
			}
			else
			{
				CustomerInfo customer = customerService.GetCustomerInfo(companyId);
				return new ObjectResult(customer);
			}
		}

		[HttpGet]
		[ActionName("getCustomerBySession")]
		public IActionResult GetCustomerInfo(string sessionId)
		{

			return new ObjectResult(true);
		}

		
		[HttpGet]
		[ActionName("getCustomerContractData")]
		public IActionResult GetCustomerContractData(int companyId)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken, companyId);

			if(sessionState != SessionService.SESSION_STATE.MANAGER)
				return StatusCode(401);

			List<ContractData> contractList = customerService.GetContractData(companyId);
			return new ObjectResult(contractList);
		}

		[HttpGet]
		[ActionName("validateSession")]
		public IActionResult ValidateSession(int companyId)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken, companyId);

			if(sessionState == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			return new ObjectResult(sessionState);
		}
	}
}