using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using DeactivationService.Models;

namespace DeactivationService.Procs
{
	public class UpdateRequestReasonProc : AbstractStoredProcCall
	{
		public UpdateRequestReasonProc(string connStr): base("", connStr) { }

		public bool Execute(UpdateReason update)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@requestId", update.requestId);
			this.SqlParams.Add("@intDsn", update.dsn);
			this.SqlParams.Add("@intReason", update.reason);

			DataTable dt = base.Execute();
			List<bool> responseList = base.MapBool(dt);

			return (responseList?.Count > 0)? responseList[0] : false;
		}
	}
}
