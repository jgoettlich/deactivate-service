using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using DeactivationService.Models;

namespace DeactivationService.Procs
{
	public class GetDeactivationRequestListProc : AbstractStoredProcCall
	{
		public GetDeactivationRequestListProc(string connectionString) : base("deactivate_get_request_list", connectionString)
		{
		}

		public List<DeactivateReport> Execute(int cid, int page, int pageSize, bool showOnlyPending)
		{
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intPage", page);
			this.SqlParams.Add("@intPageSize", pageSize);
			this.SqlParams.Add("@bitOnlyPending", showOnlyPending);

			DataTable dt = base.Execute();
			IEnumerable<DeactivateReport> requests = base.MapData(dt, typeof(DeactivateReport)).Cast<DeactivateReport>();
			List<DeactivateReport> reportList = requests.ToList();

			return reportList;
		}
	}
}
