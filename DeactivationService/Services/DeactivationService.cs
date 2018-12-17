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
		UpdateRequestStatusProc			updateRequestStatusProc;

		public DeviceDeactivationService(IConfiguration configuration)
		{
			var connString = configuration.GetConnectionString("mainDatabase");

			createDeactivateRequestProc		= new CreateDeactivateRequestProc(connString);
			getDeactivationRequestListProc	= new GetDeactivationRequestListProc(connString);
			cancelDeactivateRequestProc		= new CancelDeactivateRequestProc(connString);
			updateRequestStatusProc			= new UpdateRequestStatusProc(connString);
		}

		public bool DeactivatDevice(int vid, int cid, string trucknum, int dsn, int status, int userId)
		{
			List<bool> resp = createDeactivateRequestProc.Execute(vid, cid, trucknum, dsn, status, userId);

			return (resp?.Count > 0) ? resp[0] : false;
		}

		public List<DeactivateReport> GetDeactivationReport(int cid, int page, int pageSize, bool showOnlyPending)
		{
			return getDeactivationRequestListProc.Execute(cid, page, pageSize, showOnlyPending);
		}

		public bool CancelRequest(int cid, int dsn)
		{
			List<bool> respList = cancelDeactivateRequestProc.Execute(cid, dsn);

			return (respList?.Count > 0)? respList[0] : false;
		}

		public bool UpdateRequestStatus(int cid, int dsn, int status)
		{
			List<bool> respList = updateRequestStatusProc.Execute(cid, dsn, status);

			return (respList?.Count > 0) ? respList[0] : false;
		}
	}
}
