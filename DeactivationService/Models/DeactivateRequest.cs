using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class DeactivateRequest
	{
		public System.Guid? requestId { get; set; }
		public string sessionId { get; set; }
		public int? userId { get; set; }
		public int cid { get; set; }
		public int? reason { get; set; }
		public int? status { get; set; }
		public string cm_notes { get; set; }
		public string cust_notes { get; set; }
		public string username { get; set; }
		public DateTime? requestDate { get; set; }
		public DateTime? completedDate { get; set; }
		public List<Device> deviceList { get; set; }

		public DeactivateRequest()
		{
			userId			= null;
			reason			= null;
			status			= null;
			cm_notes		= null;
			cust_notes		= null;
			requestDate		= null;
			completedDate	= null;
		}
	}
}
