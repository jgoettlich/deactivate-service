using DeactivationService.Models;
using DeactivationService.Procs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Services
{
	public class DeviceService
	{
		GetDeviceListProc getDeviceListProc;
		GetDeviceFromTrucknumProc getDeviceFromTrucknumProc;
		public DeviceService(IConfiguration configuration)
		{
			var connString = configuration.GetConnectionString("mainDatabase");
			getDeviceListProc = new GetDeviceListProc(connString);
			getDeviceFromTrucknumProc = new GetDeviceFromTrucknumProc(connString);
		}

		public List<Device> GetDeviceList(int cid, int page, int pageSize)
		{
			return getDeviceListProc.Execute(cid, page, pageSize);
		}

		public List<Device> GetDeviceFromTrucknum(int cid, string trucknum)
		{
			List<Device> deviceList = getDeviceFromTrucknumProc.Execute(cid, trucknum);
			if (deviceList.Count == 0)
			{
				Device d = new Device();
				d.cid = cid;
				d.trucknum = trucknum;

				deviceList.Add(d);
			}
			return deviceList;
		}
	}
}
