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

		public List<bool> Execute(int vid, int cid, string trucknum, int dsn, int status, int userId)
		{
			this.SqlParams.Add("@intvid", vid);
			this.SqlParams.Add("@intcid", cid);
			this.SqlParams.Add("@strtrucknum", trucknum);
			this.SqlParams.Add("@intdsn", dsn);
			this.SqlParams.Add("@intstatus", status);
			this.SqlParams.Add("@intuserId", userId);

			DataTable dt = base.Execute();
			List<bool> successList = new List<bool>();
			foreach (DataRow row in dt.Rows)
			{
				bool val = (row[0] == DBNull.Value) ? false : Convert.ToBoolean(row[0]);
				successList.Add(val);
			}

			return successList;
		}
	}
}
