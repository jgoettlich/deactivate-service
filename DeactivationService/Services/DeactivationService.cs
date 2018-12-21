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

		public DeviceDeactivationService(IConfiguration configuration)
		{
			var connString = configuration.GetConnectionString("mainDatabase");

			createDeactivateRequestProc		= new CreateDeactivateRequestProc(connString);
			getDeactivationRequestListProc	= new GetDeactivationRequestListProc(connString);
			cancelDeactivateRequestProc		= new CancelDeactivateRequestProc(connString);
			updateRequestProc				= new UpdateRequestProc(connString);
			addDeviceToRequestProc			= new AddDeviceToRequestProc(connString);
			getRequestDeviceListProc		= new GetRequestDeviceListProc(connString);
		}

		public List<DeactivateResponse> Deactivate(DeactivateRequest request)
		{
			List<string> resp = createDeactivateRequestProc.Execute(request.cid, request.status, request.reason, request.userId, request.cm_notes, request.cust_notes);
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

		public List<DeactivateRequest> GetDeactivationReport(int cid, int page, int pageSize, bool showOnlyPending)
		{
			List<DeactivateRequest> requestList = getDeactivationRequestListProc.Execute(cid, page, pageSize, showOnlyPending);
			foreach(DeactivateRequest r in requestList)
			{
				List<Device> deviceList = getRequestDeviceListProc.Execute(r.requestId.ToString());
				r.deviceList = deviceList;
			}

			return requestList;
		}

		public bool CancelRequest(int cid, int dsn)
		{
			List<bool> respList = cancelDeactivateRequestProc.Execute(cid, dsn);

			return (respList?.Count > 0)? respList[0] : false;
		}

		public bool UpdateRequest(string requestId, int status, int reason, string cmNotes, string custNotes)
		{
			List<bool> respList = updateRequestProc.Execute(requestId, status, reason, cmNotes, custNotes);

			return (respList?.Count > 0) ? respList[0] : false;
		}
	}
}
