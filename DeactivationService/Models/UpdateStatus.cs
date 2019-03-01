using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class UpdateStatus
	{
		public string	requestId	{ get; set; }
		public int?		dsn			{ get; set; }
		public int?		status		{ get; set; }
	}
}
