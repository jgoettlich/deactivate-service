using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class SessionData
	{
		public int		cid				{ get; set; }
		public string	service_list	{ get; set; }
		public int		uid				{ get; set; }


		public bool HasService(int serviceId)
		{
			if(service_list == null)
				return false;

			string[] services = service_list.Replace(" ", "").Split(";");

			return services.Contains(serviceId.ToString());
		}
	}
}
