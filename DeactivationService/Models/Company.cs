using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class Company : Object
	{
		public int? cid { get; set; }
		public string companyName { get; set; }

		public Company()
		{
			cid			= null;
			companyName = null;
		}
	}
}
