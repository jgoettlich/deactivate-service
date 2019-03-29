using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeactivationService.Models;
using DeactivationService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace DeactivationService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
		DeviceService	deviceService;
		SessionService	sessionService;

		public DeviceController(IConfiguration configuration, IMemoryCache memoryCache)
		{
			deviceService	= new DeviceService(configuration);
			sessionService	= new SessionService(configuration, memoryCache);
		}

		[HttpGet]
		[ActionName("getDeviceList")]
		public IActionResult GetDeviceList(int companyId, int page, int pageSize)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken, companyId);

			if(sessionState == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			List<Device> report = deviceService.GetDeviceList(companyId, page, pageSize);
			return new ObjectResult(report);
		}

		[HttpGet]
		[ActionName("getDevice")]
		public IActionResult GetDevice(int companyId, string trucknum)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken, companyId);

			if(sessionState == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			List<Device> report = deviceService.GetDeviceFromTrucknum(companyId, trucknum);
			return new ObjectResult(report);
		}
	}
}