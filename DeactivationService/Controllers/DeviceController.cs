using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeactivationService.Models;
using DeactivationService.Services;
using Microsoft.Extensions.Configuration;

namespace DeactivationService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
		DeviceService deviceService;

		public DeviceController(IConfiguration configuration)
		{
			deviceService = new DeviceService(configuration);
		}

		[HttpGet]
		[ActionName("getDeviceList")]
		public IActionResult GetDeviceList(int companyId, int page, int pageSize)
		{
			List<Device> report = deviceService.GetDeviceList(companyId, page, pageSize);
			return new ObjectResult(report);
		}

		[HttpGet]
		[ActionName("getDevice")]
		public IActionResult GetDevice(int companyId, string trucknum)
		{
			List<Device> report = deviceService.GetDeviceFromTrucknum(companyId, trucknum);
			return new ObjectResult(report);
		}
	}
}