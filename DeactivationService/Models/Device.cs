using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class Device
	{
		public int? cid { get; set; }
		public int? dsn { get; set; }
		public int vid { get; set; }
		public string trucknum { get; set; }
		public int? status { get; set; }
		public int? reason { get; set; }

		public Device()
		{
			cid			= null;
			dsn			= null;
			trucknum	= null;
			status		= 0;
			reason		= null;
		}
	}
}
