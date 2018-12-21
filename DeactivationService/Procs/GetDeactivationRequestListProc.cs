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

		public List<DeactivateRequest> Execute(int cid, int page, int pageSize, bool showOnlyPending)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intPage", page);
			this.SqlParams.Add("@intPageSize", pageSize);
			this.SqlParams.Add("@bitOnlyPending", showOnlyPending);

			DataTable dt = base.Execute();
			IEnumerable<DeactivateRequest> requests = base.MapData(dt, typeof(DeactivateRequest)).Cast<DeactivateRequest>();
			List<DeactivateRequest> reportList = requests.ToList();

			return reportList;
		}
	}
}
