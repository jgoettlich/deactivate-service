using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeactivationService.Services;
using Microsoft.Extensions.Configuration;
using DeactivationService.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DeactivationService.Controllers
{
	[Produces("application/json")]
	[Route("api/deactivate/[action]")]
    [ApiController]
    public class DeactivationController : ControllerBase
    {
		DeviceDeactivationService	deactivationService;
		SessionService				sessionService;

		public DeactivationController(IConfiguration configuration, IMemoryCache memoryCache)
		{
			deactivationService = new DeviceDeactivationService(configuration);
			sessionService		= new SessionService(configuration, memoryCache);
		}

		[HttpGet]
		[ActionName("getReport")]
		public IActionResult GetReport(int companyId, int page, int pageSize, 
			bool onlyPending, string sortColumn, bool sortAsc, string filterColumn, string filterValue)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken, companyId);

			if(sessionState == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

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
		public IActionResult GetRequest(string requestId, int companyId)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken, companyId);

			if(sessionState == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			DeactivateRequest report = deactivationService.GetDeactivationRequest(requestId);
			return new ObjectResult(report);
		}

		[HttpGet]
		[ActionName("getStatusSummary")]
		public IActionResult GetStatusSummary(int companyId)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE sessionState = sessionService.CheckSession(authToken, companyId);

			if(sessionState == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			List<StatusSummary> summary = deactivationService.GetStatusSummary(companyId);

			return new ObjectResult(summary);
		}

		[HttpPost]
		[ActionName("deactivateDevice")]
		public IActionResult DeactivateDevice([FromBody] DeactivateRequest request)
		{
			// Verify the user request
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE state = sessionService.CheckSession(authToken, request.cid);
			request.userId = sessionService.GetUserId(authToken);

			if(state == SessionService.SESSION_STATE.INVALID || request.userId == -1)
				return StatusCode(401);

			string requestId = deactivationService.Deactivate(request);

			return new ObjectResult(requestId);
		}

		[HttpPost]
		[ActionName("removeDeviceFromRequest")]
		public IActionResult RemoveDeviceFromRequest([FromBody] Device device)
		{
			if (device == null || device.cid == null || device.dsn == null || device.requestId == null)
			{
				return StatusCode(400);
			}

			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE state = sessionService.CheckSession(authToken, Convert.ToInt32(device.cid));

			// TODO: Validate status vs session
			if(state == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			bool response = deactivationService.RemoveDeviceFromRequest(Convert.ToInt32(device.cid), Convert.ToInt32(device.dsn), device.requestId);
			return new ObjectResult(response);
		}

		[HttpPost]
		[ActionName("cancelRequest")]
		public IActionResult CancelRequest([FromBody] DeactivateRequest request)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE state = sessionService.CheckSession(authToken, Convert.ToInt32(request.cid));

			// TODO: Validate status vs session
			if(state == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			if (request.requestId == null || request.requestId == null)
			{
				return StatusCode(400);
			}

			bool response = deactivationService.CancelRequest(request.requestId.ToString());

			return new ObjectResult(response);
		}

		[HttpPost]
		[ActionName("updateRequest")]
		public IActionResult UpdateRequest([FromBody] DeactivateRequest request)
		{
			if (request == null)
				return StatusCode(400);

			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE state = sessionService.CheckSession(authToken, Convert.ToInt32(request.cid));

			// TODO: Validate status vs session
			if(state == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

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
		[ActionName("updateRequestStatus")]
		public StatusCodeResult UpdateRequestStatus(UpdateStatus update)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE state = sessionService.CheckSession(authToken, Convert.ToInt32(update.cid));

			// TODO: Validate status vs session
			if(state == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			bool success = this.deactivationService.UpdateRequestStatus(update);

			return (success)? StatusCode(200) : StatusCode(400);
		}

		[HttpPost]
		[ActionName("updateRequestReason")]
		public StatusCodeResult UpdateRequestReason(UpdateReason update)
		{
			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE state = sessionService.CheckSession(authToken, Convert.ToInt32(update.cid));

			// TODO: Validate status vs session
			if(state == SessionService.SESSION_STATE.INVALID)
				return StatusCode(401);

			bool success = this.deactivationService.UpdateRequestReason(update);

			return (success)? StatusCode(200) : StatusCode(400);
		}

		[HttpPost]
		[ActionName("updateDeviceFees")]
		public IActionResult UpdateDeviceFees([FromBody] List<Device> deviceList)
		{
			if(deviceList.Count == 0)
				return StatusCode(400);

			string authToken = Request.Headers["x-access-token"];
			SessionService.SESSION_STATE state = sessionService.CheckSession(authToken, Convert.ToInt32(deviceList[0].cid));

			if(state != SessionService.SESSION_STATE.MANAGER)
				return StatusCode(401);

			List<bool> response = deactivationService.UpdateDeviceFees(deviceList);
			return new ObjectResult(response);
		}
	}
}