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

		public List<string> Execute(int cid, int? status, int? reason, int? userId, string cmNotes, string custNotes)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intstatus", status);
			this.SqlParams.Add("@intReason", reason);
			this.SqlParams.Add("@intuserId", userId); 
			this.SqlParams.Add("@strCmNotes", cmNotes);
			this.SqlParams.Add("@strCustNotes", custNotes);

			 DataTable dt = base.Execute();
			List<string> requestIdList = new List<string>();
			foreach (DataRow row in dt.Rows)
			{
				string requestId = (row[0] == DBNull.Value) ? null : Convert.ToString(row[0]);
				requestIdList.Add(requestId);
			}

			return requestIdList;
		}
	}
}
