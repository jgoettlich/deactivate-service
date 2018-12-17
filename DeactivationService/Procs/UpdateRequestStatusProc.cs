using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeactivationService.Models;
using System.Data;

namespace DeactivationService.Procs
{
	public class UpdateRequestStatusProc : AbstractStoredProcCall
	{
		public UpdateRequestStatusProc(string connectionStr) : base("deactivate_update_request_status", connectionStr)
		{

		}

		public List<bool> Execute(int cid, int dsn, int status)
		{
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intDsn", dsn);
			this.SqlParams.Add("@intStatus", status);

			DataTable dt = base.Execute();
			IEnumerable<bool> reponse = base.MapData(dt, typeof(bool)).Cast<bool>();
			List<bool> responseList = reponse.ToList();

			return responseList;
		}
	}
}
