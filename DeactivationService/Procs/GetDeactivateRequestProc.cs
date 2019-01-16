using DeactivationService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Procs
{
	public class GetDeactivateRequestProc : AbstractStoredProcCall
	{

		public GetDeactivateRequestProc(string connStr) : base("deactivate_get_request", connStr)
		{

		}

		public DeactivateRequest Execute(string requestId)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@requestId", requestId);

			DataTable dt = base.Execute();
			IEnumerable<DeactivateRequest> reponse = base.MapData(dt, typeof(DeactivateRequest)).Cast<DeactivateRequest>();
			List<DeactivateRequest> responseList = reponse.ToList();

			if (responseList != null && responseList.Count > 0) { 
				return responseList[0];	
			}

			return null;
		}
	}
}
