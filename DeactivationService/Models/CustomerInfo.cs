using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class CustomerInfo
	{
		public int companyId { get; set; }
		public string companyName { get; set; }
		public int parentId { get; set; }
		public string parentName { get; set; }
		public int adjustedObcCount { get; set; }
		public int obcCount { get; set; }
		public int safetyStock { get; set; }
		public string customerManager { get; set; }
		public string ramNasVas { get; set; }
		public string salesRep { get; set; }
		public string customerType { get; set; }
		public string ratePlan { get; set; }
		public int unitSuspendedMonths { get; set; }
		public int returnOnly { get; set; }
		public int standardRma { get; set; }
		public int mmUpgrade { get; set; }
		public bool predatesNetsuite { get; set; }
		public string obcCountAsOf { get; set; }
		public string custDateAsOf { get; set; }
		public bool contactReviewComplete { get; set; }
		public string lastReviewDate { get; set; }
		public string reviewNotes { get; set; }
		public double unitContractDiff { get; set; }
	}
}
