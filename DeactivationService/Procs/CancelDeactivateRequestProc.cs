﻿using System;
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

		public List<bool> Execute(string requestId)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@requestId", requestId);

			DataTable dt = base.Execute();
			List<bool> responseList = base.MapBool(dt);

			return responseList;
		}
	}
}
