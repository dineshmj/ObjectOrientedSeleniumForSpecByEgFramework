using System;
using System.ComponentModel;

namespace OOSelenium.TestWebApp.Models
{
	public class QuoteModel
	{
		[DisplayName ("Quote ID")]
		public int QuoteId { get; set; }

		[DisplayName ("Vehicle Type")]
		public string VehicleType { get; set; }

		[DisplayName ("From Date")]
		public DateTime	FromDate { get; set; }

		[DisplayName ("To Date")]
		public DateTime ToDate { get; set; }

		[DisplayName ("Car Price")]
		public double CarPrice { get; set; }

		[DisplayName ("Approx. Premium Amount")]
		public double ApproximatePremiumAmount { get; set; }

		public string Issuer { get; set; }
	}
}