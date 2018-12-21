using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class DeactivateResponse
	{
		public int? dsn { get; set; }
		public string trucknum { get; set; }
		public bool success { get; set; }

		public DeactivateResponse() { }

		public DeactivateResponse(int? dsn, string trucknum, bool success)
		{
			this.dsn = dsn;
			this.trucknum = trucknum;
			this.success = success;
		}
	}
}
