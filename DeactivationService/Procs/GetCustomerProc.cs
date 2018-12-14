using DeactivationService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Procs
{
	public class GetCustomerProc : AbstractStoredProcCall
	{
		public GetCustomerProc(string connectionString) : base("deactivate_get_customer", connectionString)
		{

		}

		public List<Customer> Execute(int companyId)
		{
			this.SqlParams.Add("@intCid", companyId);

			DataTable dt = base.Execute();
			IEnumerable<Customer> customers = base.MapData(dt, typeof(Customer)).Cast<Customer>();
			List<Customer> customerList = customers.ToList();

			return customerList;
		}
	}
}
