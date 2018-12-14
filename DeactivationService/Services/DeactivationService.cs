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
		CreateDeactivateRequestProc createDeactivateRequestProc;
		GetDeactivationRequestListProc getDeactivationRequestListProc;
		public DeviceDeactivationService(IConfiguration configuration)
		{
			var connString = configuration.GetConnectionString("mainDatabase");
			createDeactivateRequestProc = new CreateDeactivateRequestProc(connString);
			getDeactivationRequestListProc = new GetDeactivationRequestListProc(connString);
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
	}
}
