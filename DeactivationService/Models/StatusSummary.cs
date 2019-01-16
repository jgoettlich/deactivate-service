using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class StatusSummary
	{
		public int status { get; set; }
		public int requestCount { get; set; }
		public int deviceCount { get; set; }
	}
}
