using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using DeactivationService.Models;

namespace DeactivationService.Procs
{
	public class GetRequestReportByStatusProc : AbstractStoredProcCall
	{
		public GetRequestReportByStatusProc(string connectionString) : base("deactivate_get_report_by_status", connectionString)
		{
		}

		public List<DeactivateRequest> Execute(int cid, int page, int pageSize, string sortColumn, bool sortAsc, int statusFilter)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intPage", page);
			this.SqlParams.Add("@intPageSize", pageSize);
			this.SqlParams.Add("@sortColumn", sortColumn);
			this.SqlParams.Add("@bitSortAsc", sortAsc);
			this.SqlParams.Add("@statusFilter", statusFilter);

			DataTable dt = base.Execute();
			IEnumerable<DeactivateRequest> requests = base.MapData(dt, typeof(DeactivateRequest)).Cast<DeactivateRequest>();
			List<DeactivateRequest> reportList = requests.ToList();

			return reportList;
		}
	}
}
