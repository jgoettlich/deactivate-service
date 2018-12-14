using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class Device
	{
		public int cid { get; set; }
		public int dsn { get; set; }
		public int vid { get; set; }
		public string trucknum { get; set; }
		public int status { get; set; }
	}
}
