using DeactivationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace DeactivationService.Procs
{
	public class StatusSummaryProc : AbstractStoredProcCall
	{

		public StatusSummaryProc(string connStr): base("deactivate_status_summary", connStr){ }

		public List<StatusSummary> Execute(int companyId)
		{
			this.SqlParams.Clear();
			SqlParams.Add("@intCid", companyId);

			DataTable dt = base.Execute();
			IEnumerable<StatusSummary> summary = base.MapData(dt, typeof(StatusSummary)).Cast<StatusSummary>();
			List<StatusSummary> summaryList = summary.ToList();

			return summaryList;
		}
	}
}
