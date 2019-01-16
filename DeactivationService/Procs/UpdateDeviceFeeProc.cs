using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace DeactivationService.Procs
{
	public class UpdateDeviceFeeProc: AbstractStoredProcCall
	{

		public UpdateDeviceFeeProc(string connStr) : base("deactivate_update_device_fee", connStr){ }

		public bool Execute(string requestId, int cid, int dsn, decimal fee)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intDsn", dsn);
			this.SqlParams.Add("@requestId", requestId);
			this.SqlParams.Add("@decFee", fee);

			DataTable dt = base.Execute();
			List<bool> responseList = base.MapBool(dt);

			return (responseList.Count == 0)? false : responseList[0];
		}
	}
}
