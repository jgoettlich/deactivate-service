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

		public List<Company> Execute(int companyId)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intCid", companyId);

			DataTable dt = base.Execute();
			IEnumerable<Company> customers = base.MapData(dt, typeof(Company)).Cast<Company>();
			List<Company> customerList = customers.ToList();

			return customerList;
		}
	}
}
