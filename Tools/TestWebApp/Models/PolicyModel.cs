using System.ComponentModel;

namespace SimpleQA.TestWebApp.Models
{
	public class PolicyModel
		: ProposalModel
	{
		[DisplayName ("Policy ID")]
		public int PolicyId { get; set; }

		[DisplayName ("Policy Approver")]
		public string PolicyApprover { get; set; }
	}
}