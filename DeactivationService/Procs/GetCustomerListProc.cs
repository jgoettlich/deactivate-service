using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using DeactivationService.Models;

namespace DeactivationService.Procs
{
	public class GetCustomerListProc : AbstractStoredProcCall
	{

		public GetCustomerListProc(string connectionString) : base("deactivate_get_customer_list", connectionString)
		{
			
		}

		public List<Company> Execute(int page, int pageSize, string query)
		{
			this.SqlParams.Clear();
			this.SqlParams.Add("@intPage", page);
			this.SqlParams.Add("@intPageSize", pageSize);
			this.SqlParams.Add("@query", query);

			DataTable dt = base.Execute();
			IEnumerable<Company> customers = base.MapData(dt, typeof(Company)).Cast<Company>();
			List<Company> customerList = customers.ToList();

			return customerList;
		}
	}
}
