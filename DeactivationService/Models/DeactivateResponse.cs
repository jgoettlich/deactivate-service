using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class DeactivateResponse
	{
		public string trucknum { get; set; }
		public string success { get; set; }

		public DeactivateResponse() { }

		public DeactivateResponse(string trucknum, bool success)
		{
			this.trucknum = trucknum;
			this.success = (success) ? "Success" : "Failed";
		}
	}
}
