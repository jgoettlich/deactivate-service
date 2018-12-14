using DeactivationService.Models;
using DeactivationService.Procs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Services
{
	public class CustomerService
	{
		GetCustomerListProc getCustomerListProc;
		GetCustomerProc getCustomerProc;

		public CustomerService(IConfiguration configuration)
		{
			var connString = configuration.GetConnectionString("mainDatabase");
			getCustomerListProc = new GetCustomerListProc(connString);
			getCustomerProc = new GetCustomerProc(connString);
		}

		public List<Customer> GetCustomerList(int page, int pageSize, string query)
		{
			return getCustomerListProc.Execute(page, pageSize, query);
		}

		public Customer GetCustomer(int companyId)
		{
			List<Customer> cList = getCustomerProc.Execute(companyId);
			if(cList != null && cList.Count > 0)
				return cList[0];

			return new Customer();
		}

		public CustomerInfo GetCustomerInfo(int companyId)
		{
			//TODO: Get real info
			CustomerInfo info = new CustomerInfo();
			info.companyId = 57;
			info.companyName = "PeopleNet";
			info.adjustedObcCount = 1500;
			info.obcCount = 1505;
			info.safetyStock = 20;
			info.customerManager = "Tim Tim";
			info.ramNasVas = "Jess Jess";
			info.salesRep = "Joe Joe";
			info.customerType = "Enterprise";
			info.ratePlan = "PNet-Cust/Pfm&Safe/25-MU";
			info.unitSuspendedMonths = 199;
			info.returnOnly = 2;
			info.standardRma = 12;
			info.mmUpgrade = 0;
			info.predatesNetsuite = false;
			info.obcCountAsOf = "12/13/2018";
			info.custDateAsOf = "11/13/2018";
			info.contactReviewComplete = true;
			info.reviewNotes = "Unbilled OEM";
			info.unitContractDiff = 18.5;

			return info;
		}
	}
}
