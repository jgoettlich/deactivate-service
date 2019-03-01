using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Procs
{
	public class CreateDeactivateRequestProc: AbstractStoredProcCall
	{
		public CreateDeactivateRequestProc(string connectionString) : base("deactivate_create_request", connectionString)
		{
		}

		public string Execute(int cid, int? status, int? reason, int? userId, string cmNotes, string custNotes, DateTime? requestedDate)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intstatus", status);
			this.SqlParams.Add("@intReason", reason);
			this.SqlParams.Add("@intuserId", userId); 
			this.SqlParams.Add("@strCmNotes", cmNotes);
			this.SqlParams.Add("@strCustNotes", custNotes);
			this.SqlParams.Add("@requestedDate", requestedDate);

			DataTable dt = base.Execute();
			string requestId = null;
			if(dt != null && dt.Rows.Count > 0)
			{
				requestId = (dt.Rows[0][0] == DBNull.Value) ? null : Convert.ToString(dt.Rows[0][0]);
			}

			return requestId;
		}
	}
}
