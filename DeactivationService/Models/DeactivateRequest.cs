using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class DeactivateRequest
	{
		public string sessionId { get; set; }
		public int userId { get; set; }
		public List<Device> deviceList { get; set; }
	}
}
