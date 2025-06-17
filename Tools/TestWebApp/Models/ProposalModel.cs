using System.ComponentModel;

namespace OOSelenium.TestWebApp.Models
{
	public class ProposalModel
		: QuoteModel
	{
		[DisplayName ("Proposal ID")]
		public int ProposalId { get; set; }

		[DisplayName ("Proposal Initiator")]
		public string ProposalInitiator { get; set; }
	}
}