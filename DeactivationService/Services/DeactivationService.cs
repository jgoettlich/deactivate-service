using DeactivationService.Models;
using DeactivationService.Procs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Services
{
	public class DeviceDeactivationService
	{
		CreateDeactivateRequestProc		createDeactivateRequestProc;
		GetDeactivationRequestListProc	getDeactivationRequestListProc;
		CancelDeactivateRequestProc		cancelDeactivateRequestProc;
		UpdateRequestProc				updateRequestProc;
		AddDeviceToRequestProc			addDeviceToRequestProc;
		GetRequestDeviceListProc		getRequestDeviceListProc;
		GetDeactivateRequestProc		getDeactivateRequestProc;
		RemoveDeviceProc				removeDeviceProc;
		UpdateDeviceFeeProc				updateDeviceFeeProc;
		StatusSummaryProc				statusSummaryProc;

		public DeviceDeactivationService(IConfiguration configuration)
		{
			var connString = configuration.GetConnectionString("mainDatabase");

			createDeactivateRequestProc		= new CreateDeactivateRequestProc(connString);
			getDeactivationRequestListProc	= new GetDeactivationRequestListProc(connString);
			cancelDeactivateRequestProc		= new CancelDeactivateRequestProc(connString);
			updateRequestProc				= new UpdateRequestProc(connString);
			addDeviceToRequestProc			= new AddDeviceToRequestProc(connString);
			getRequestDeviceListProc		= new GetRequestDeviceListProc(connString);
			getDeactivateRequestProc		= new GetDeactivateRequestProc(connString);
			removeDeviceProc				= new RemoveDeviceProc(connString);
			updateDeviceFeeProc				= new UpdateDeviceFeeProc(connString);
			statusSummaryProc				= new StatusSummaryProc(connString);
		}

		public List<DeactivateResponse> Deactivate(DeactivateRequest request)
		{
			List<string> resp = createDeactivateRequestProc.Execute(request.cid, 
				request.status, 
				request.reason, 
				request.userId, 
				request.cm_notes, 
				request.cust_notes,
				request.requestedDate
				);
			List<DeactivateResponse> deviceResp = new List<DeactivateResponse>();
			string requestId = (resp?.Count > 0)? resp[0] : null;

			foreach (Device d in request.deviceList) {
				if (requestId != null)
				{
					bool success = addDeviceToRequestProc.Execute(requestId, d.cid, d.dsn, d.vid, d.trucknum, d.status, d.reason);
					deviceResp.Add(new DeactivateResponse(d.dsn, d.trucknum, success));
				}
				else
				{
					deviceResp.Add(new DeactivateResponse(d.dsn, d.trucknum, false));
				}
			}
			
			return deviceResp;
		}

		public List<DeactivateRequest> GetDeactivationReport(int cid, int page, int pageSize, bool showOnlyPending, string sortColumn, bool sortAsc)
		{
			List<DeactivateRequest> requestList = getDeactivationRequestListProc.Execute(cid, page, pageSize, showOnlyPending, sortColumn, sortAsc);
			foreach(DeactivateRequest r in requestList)
			{
				List<Device> deviceList = getRequestDeviceListProc.Execute(r.requestId.ToString());
				r.deviceList = deviceList;
			}

			return requestList;
		}

		public DeactivateRequest GetDeactivationRequest(string requestId)
		{
			DeactivateRequest request = getDeactivateRequestProc.Execute(requestId);
			List<Device> deviceList = getRequestDeviceListProc.Execute(requestId);
			request.deviceList = deviceList;

			return request;
		}

		public List<StatusSummary> GetStatusSummary(int companyId)
		{
			return statusSummaryProc.Execute(companyId);
		}

		public bool RemoveDeviceFromRequest(int cid, int dsn, string requestId)
		{
			List<bool> respList = removeDeviceProc.Execute(cid, dsn, requestId);

			return (respList?.Count > 0) ? respList[0] : false;
		}

		public bool CancelRequest(string requestId)
		{
			List<bool> respList = cancelDeactivateRequestProc.Execute(requestId);

			return (respList?.Count > 0)? respList[0] : false;
		}

		public bool UpdateRequest(string requestId, int status, int reason, string cmNotes, string custNotes, decimal fee)
		{
			List<bool> respList = updateRequestProc.Execute(requestId, status, reason, cmNotes, custNotes, fee);

			return (respList?.Count > 0) ? respList[0] : false;
		}

		public List<bool> UpdateDeviceFees(List<Device> deviceList)
		{
			List<bool> respList = new List<bool>();
			foreach(Device d in deviceList)
			{ 
				bool resp = updateDeviceFeeProc.Execute(d.requestId, Convert.ToInt32(d.cid), Convert.ToInt32(d.dsn), Convert.ToDecimal(d.fee));
				respList.Add(resp);
			}
				

			return respList;
		}
	}
}
