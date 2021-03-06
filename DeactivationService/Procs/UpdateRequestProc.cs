﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeactivationService.Models;
using System.Data;

namespace DeactivationService.Procs
{
	public class UpdateRequestProc : AbstractStoredProcCall
	{
		public UpdateRequestProc(string connectionStr) : base("deactivate_update_request", connectionStr)
		{

		}

		public List<bool> Execute(string requestId, int status, int reason, string cmNotes, string custNotes, decimal fee)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@requestId", requestId);
			this.SqlParams.Add("@intStatus", status);
			this.SqlParams.Add("@intReason", reason);
			this.SqlParams.Add("@strCmNotes", cmNotes);
			this.SqlParams.Add("@strCustNotes", custNotes);
			this.SqlParams.Add("@intFee", fee);

			DataTable dt = base.Execute();
			List<bool> responseList = base.MapBool(dt);

			return responseList;
		}
	}
}
