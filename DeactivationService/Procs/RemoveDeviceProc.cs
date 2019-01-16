using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace DeactivationService.Procs
{
	public class RemoveDeviceProc : AbstractStoredProcCall
	{
		public RemoveDeviceProc(string connectionStr) : base("deactivate_remove_device_from_request", connectionStr)
		{
		}

		public List<bool> Execute(int cid, int dsn, string requestId)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intDsn", dsn);
			this.SqlParams.Add("@requestId", requestId);

			DataTable dt = base.Execute();
			List<bool> responseList = base.MapBool(dt);

			return responseList;
		}
	}
}
