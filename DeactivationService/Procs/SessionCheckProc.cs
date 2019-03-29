using DeactivationService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Procs
{
	public class SessionCheckProc : AbstractStoredProcCall
	{
		public SessionCheckProc(string connStr): base("deactivate_check_session", connStr) { }

		public SessionData Execute(int sessionId)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@sessionId", sessionId);

			DataTable dt = base.Execute();
			IEnumerable<SessionData> responseList = base.MapData(dt, typeof(SessionData)).Cast<SessionData>();
			List<SessionData> dataList = responseList.ToList();

			return (dataList?.Count > 0)? dataList[0] : null;
		}
	}
}
