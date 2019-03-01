using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeactivationService.Models
{
	public class ContractData
	{
		public int month				{ get; set; }
		public string contractType		{ get; set; }
		public DateTime contractEnd		{ get; set; }
		public int terms				{ get; set; }
		public int termsLessChurn		{ get; set; }
		public int monthsRemaining		{ get; set; }
		public int perUnitTermFee		{ get; set; }
		public int totalTermFee			{ get; set; }
	}
}
