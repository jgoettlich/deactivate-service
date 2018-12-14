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
			List<DeactivateReport> report = deactivationService.GetDeactivationReport(companyId, page, pageSize, onlyPending);
			return new ObjectResult(report);
		}

		[HttpPost]
		[ActionName("deactivateDevice")]
		public IActionResult DeactivateDevice([FromBody] DeactivateRequest request)
		{
			// Verify the user request
			request.userId = 1298352; // Replace this with converted session Id

			// Swap the DSNs
			List<DeactivateResponse> responseList = new List<DeactivateResponse>();
			List<Device> deviceList = request.deviceList;
			for (int i = 0; i < deviceList.Count; i++)
			{
				Device device = deviceList[i];
				Boolean response = deactivationService.DeactivatDevice(
						device.vid,
						device.cid,
						device.trucknum,
						device.dsn,
						device.status,
						request.userId
				);
				DeactivateResponse deactivateResponse = new DeactivateResponse(device.trucknum, response);
				responseList.Add(deactivateResponse);
			}

			return new ObjectResult(responseList);
		}
	}
}