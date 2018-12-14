using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class DeactivateReport
	{
		public int vid { get; set; }
		public int cid { get; set; }
		public string trucknum { get; set; }
		public int dsn { get; set; }
		public int status { get; set; }
		public string requestedDate { get; set; }
		public string completedDate { get; set; }
		public string username { get; set; }
	}
}
