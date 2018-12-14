using DeactivationService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Procs
{
	public class GetDeviceFromTrucknumProc : AbstractStoredProcCall
	{
		public GetDeviceFromTrucknumProc(string connectionString) : base("deactivate_get_device_from_trucknum", connectionString)
		{
		}

		public List<Device> Execute(int cid, string trucknum)
		{
			this.SqlParams.Add("@intCid", cid);
			this.SqlParams.Add("@stringTrucknum", trucknum);

			DataTable dt = base.Execute();
			IEnumerable<Device> devices = base.MapData(dt, typeof(Device)).Cast<Device>();
			List<Device> deviceList = devices.ToList();

			return deviceList;
		}
	}
}
