using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class UpdateReason
	{
		public string requestId { get; set; }
		public int? dsn			{ get; set; }
		public int reason		{ get; set; }
		public int cid			{ get; set; }
	}
}
