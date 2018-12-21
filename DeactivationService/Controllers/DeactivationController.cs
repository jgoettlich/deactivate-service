using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeactivationService.Services;
using Microsoft.Extensions.Configuration;
using DeactivationService.Models;

namespace DeactivationService.Controllers
{
	[Produces("application/json")]
	[Route("api/deactivate/[action]")]
    [ApiController]
    public class DeactivationController : ControllerBase
    {
		DeviceDeactivationService deactivationService;

		public DeactivationController(IConfiguration configuration)
		{
			deactivationService = new DeviceDeactivationService(configuration);
		}

		[HttpGet]
		[ActionName("getReport")]
		public IActionResult GetReport(int companyId, int page, int pageSize, bool onlyPending)
		{
			List<DeactivateRequest> report = deactivationService.GetDeactivationReport(companyId, page, pageSize, onlyPending);
			return new ObjectResult(report);
		}

		[HttpPost]
		[ActionName("deactivateDevice")]
		public IActionResult DeactivateDevice([FromBody] DeactivateRequest request)
		{
			// Verify the user request
			request.userId = 1298352; // Replace this with converted session Id

			List<DeactivateResponse> responseList = deactivationService.Deactivate(request);

			return new ObjectResult(responseList);
		}

		[HttpPost]
		[ActionName("cancelRequest")]
		public IActionResult CancelRequest([FromBody] Device request)
		{
			if (request.cid == null || request.dsn == null)
			{
				return new ObjectResult(false);
			}

			bool response = deactivationService.CancelRequest(Convert.ToInt32(request.cid), Convert.ToInt32(request.dsn));

			return new ObjectResult(response);
		}

		[HttpPost]
		[ActionName("updateRequest")]
		public IActionResult UpdateRequest([FromBody] DeactivateRequest request)
		{
			if (request == null)
			{
				return new ObjectResult(false);
			}

			bool response = deactivationService.UpdateRequest(
				request.requestId.ToString(), 
				Convert.ToInt32(request.status),
				Convert.ToInt32(request.reason),
				request.cm_notes,
				request.cust_notes
				);

			return new ObjectResult(response);
		}
	}
}