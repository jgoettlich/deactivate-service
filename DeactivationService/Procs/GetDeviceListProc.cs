using DeactivationService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Procs
{
	public class GetDeviceListProc : AbstractStoredProcCall
	{

		public GetDeviceListProc(string connectionString) : base("deactivate_get_device_list", connectionString)
		{
		}

		public List<Device> Execute(int cid, int page, int pageSize)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@intPage", page);
			this.SqlParams.Add("@intPageSize", pageSize);

			DataTable dt = base.Execute();
			IEnumerable<Device> devices = base.MapData(dt, typeof(Device)).Cast<Device>();
			List<Device> deviceList = devices.ToList();

			return deviceList;
		}
	}
}
