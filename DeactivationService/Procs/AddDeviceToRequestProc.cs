using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace DeactivationService.Procs
{
	public class AddDeviceToRequestProc : AbstractStoredProcCall
	{
		public AddDeviceToRequestProc(string connectionString) : base("deactivate_add_device_to_request", connectionString)
		{ 
		}

		public bool Execute(string requestId, int? cid, int? dsn, int? vid, string trucknum, int? status, int? reason)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@requestId", requestId);
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intDsn", dsn);
			this.SqlParams.Add("@intVid", vid);
			this.SqlParams.Add("@strTrucknum", trucknum);
			this.SqlParams.Add("@intStatus", status);
			this.SqlParams.Add("@intReason", reason);

			DataTable dt = base.Execute();
			List<bool> responseList = base.MapBool(dt);

			if (responseList != null && responseList.Count > 0) { 
				return responseList[0];	
			}

			return false;
		}
	}
}
