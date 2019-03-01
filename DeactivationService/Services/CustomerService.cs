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

		public List<Company> GetCustomerList(int page, int pageSize, string query)
		{
			return getCustomerListProc.Execute(page, pageSize, query);
		}

		public Company GetCustomer(int companyId)
		{
			List<Company> cList = getCustomerProc.Execute(companyId);
			if(cList != null && cList.Count > 0)
				return cList[0];

			return new Company();
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

		public List<ContractData> GetContractData(int companyId)
		{
			List<ContractData> dataList = new List<ContractData>();

			dataList = popFakeContract();

			return dataList;
		}

		private List<ContractData> popFakeContract() 
		{
			string[] dateList = {"1/31/2019", "1/31/2020", "1/31/2021", "1/31/2022", "1/31/2019"};
			string[] cType = { "Renewal or RPC", "Renewal or RPC", "Renewal or RPC", "Renewal or RPC", "MPA or Add-On" };
			int[] term = { 9010, 2045, 650, 366, 8594 };

			List<ContractData> dataList = new List<ContractData>();
			for(int i=0;i<dateList.Length;i++)
			{ 
				ContractData data = new ContractData();
				data.contractType = cType[i];
				data.terms = term[i];
				data.contractEnd = Convert.ToDateTime(dateList[i]);
				data.month = 1;
				dataList.Add(data);
			}

			return dataList;
		}
	}
}
