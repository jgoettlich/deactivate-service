using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using DeactivationService.Models;

namespace DeactivationService.Procs
{
	public class GetRequestDeviceListProc : AbstractStoredProcCall
	{

		public GetRequestDeviceListProc(string connectionString) : base("deactivate_get_request_device_list", connectionString)
		{ }

		public List<Device> Execute(string requestId)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@requestId", requestId);

			DataTable dt = base.Execute();
			IEnumerable<Device> reponse = base.MapData(dt, typeof(Device)).Cast<Device>();
			List<Device> responseList = reponse.ToList();

			return responseList;
		}
	}
}
