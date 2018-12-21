using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace DeactivationService.Procs
{
	public class CancelDeactivateRequestProc: AbstractStoredProcCall
	{
		public CancelDeactivateRequestProc(string connectionStr) : base("deactivate_cancel_request", connectionStr)
		{ 
		}

		public List<bool> Execute(int cid, int dsn)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intDsn", dsn);

			DataTable dt = base.Execute();
			IEnumerable<bool> reponse = base.MapData(dt, typeof(bool)).Cast<bool>();
			List<bool> responseList = reponse.ToList();

			return responseList;
		}
	}
}
