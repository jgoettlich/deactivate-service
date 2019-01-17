﻿using System;
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
		public IActionResult GetReport(int companyId, int page, int pageSize, 
			bool onlyPending, string sortColumn, bool sortAsc, string filterColumn, string filterValue)
		{
			string[] validSortColumns = { "requestDate", "status", "reason", "completedDate", "username" };
			if(!validSortColumns.Contains(sortColumn))
			{
				sortColumn = "requestDate";
			}
			List<DeactivateRequest> report = deactivationService.GetDeactivationReport(companyId, page, pageSize, onlyPending, sortColumn, sortAsc, filterColumn, filterValue);
			return new ObjectResult(report);
		}

		[HttpGet]
		[ActionName("getRequest")]
		public IActionResult GetRequest(string requestId)
		{
			DeactivateRequest report = deactivationService.GetDeactivationRequest(requestId);
			return new ObjectResult(report);
		}

		[HttpGet]
		[ActionName("getStatusSummary")]
		public IActionResult GetStatusSummary(int companyId)
		{
			List<StatusSummary> summary = deactivationService.GetStatusSummary(companyId);

			return new ObjectResult(summary);
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
		[ActionName("removeDeviceFromRequest")]
		public IActionResult RemoveDeviceFromRequest([FromBody] Device device)
		{
			if (device == null || device.cid == null || device.dsn == null || device.requestId == null)
			{
				return new ObjectResult(false);
			}

			bool response = deactivationService.RemoveDeviceFromRequest(Convert.ToInt32(device.cid), Convert.ToInt32(device.dsn), device.requestId);
			return new ObjectResult(response);
		}

		[HttpPost]
		[ActionName("cancelRequest")]
		public IActionResult CancelRequest([FromBody] DeactivateRequest request)
		{
			if (request.requestId == null || request.requestId == null)
			{
				return new ObjectResult(false);
			}

			bool response = deactivationService.CancelRequest(request.requestId.ToString());

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
				request.cust_notes,
				Convert.ToDecimal(request.fee)
				);

			return new ObjectResult(response);
		}

		[HttpPost]
		[ActionName("updateDeviceFees")]
		public IActionResult UpdateDeviceFees([FromBody] List<Device> deviceList)
		{
			List<bool> response = deactivationService.UpdateDeviceFees(deviceList);
			return new ObjectResult(response);
		}
	}
}