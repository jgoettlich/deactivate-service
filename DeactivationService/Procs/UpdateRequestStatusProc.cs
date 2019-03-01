using DeactivationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace DeactivationService.Procs
{
	public class UpdateRequestStatusProc : AbstractStoredProcCall
	{
		public UpdateRequestStatusProc(string connStr): base("deactivate_update_request_status", connStr){ }

		public bool Execute(Models.UpdateStatus status)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intStatus", status.status);
			this.SqlParams.Add("@intDsn", status.dsn);
			this.SqlParams.Add("@requestId", status.requestId);

			DataTable dt = base.Execute();
			List<bool> responseList = base.MapBool(dt);

			return (responseList?.Count > 0)? responseList[0] : false;
		}
	}
}
