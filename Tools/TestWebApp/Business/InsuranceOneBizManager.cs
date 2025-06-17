using System.Collections.Generic;
using System.Linq;
using OOSelenium.TestWebApp.Business.Entities;
using OOSelenium.TestWebApp.Models;

namespace OOSelenium.TestWebApp.Business
{
	public static class InsuranceOneBizManager
	{
		private static int quotesCounter = 10;
		private static int proposalCounter = 7;
		private static int policyCounter = 4;

		public static IList<SignInModel> Users
			= new List<SignInModel>
				{
					new SignInModel { UserId = "admin", EncryptedPassword = "123", UserRole = UserRole.Admin },
					new SignInModel { UserId = "quote_issuer1", EncryptedPassword = "123", UserRole = UserRole.QuoteIssuer },
					new SignInModel { UserId = "proposer1", EncryptedPassword = "123", UserRole = UserRole.ProposalInitiator },
					new SignInModel { UserId = "policy_approver1", EncryptedPassword = "123", UserRole = UserRole.PolicyApprover }
				};

		private static IList<string> vehicleTypes = new List<string> { "Sedan", "Caravan", "Hybrid", "Convertible", "Hatchback", "Luxury Sedan" };

		public static IList<QuoteModel> Quotes
			= new List<QuoteModel>
				{ 
					new QuoteModel { QuoteId = 7, VehicleType = "Sedan", CarPrice = 5000, FromDate = new System.DateTime (2020, 4, 1), ToDate = new System.DateTime (2020, 8, 31), ApproximatePremiumAmount = 100, Issuer = "quote_issuer1" },
					new QuoteModel { QuoteId = 8, VehicleType = "Sedan", CarPrice = 4500, FromDate = new System.DateTime (2020, 4, 10), ToDate = new System.DateTime (2020, 9, 9), ApproximatePremiumAmount = 90, Issuer = "quote_issuer1" },
					new QuoteModel { QuoteId = 9, VehicleType = "Caravan", CarPrice = 7200, FromDate = new System.DateTime (2020, 4, 20), ToDate = new System.DateTime (2020, 9, 19), ApproximatePremiumAmount = 144, Issuer = "quote_issuer1" }
				};

		public static IList<ProposalModel> Proposals
			= new List<ProposalModel>
				{
					new ProposalModel { ProposalId = 4, QuoteId = 4, VehicleType = "Hybrid", CarPrice = 5500, FromDate = new System.DateTime (2020, 4, 1), ToDate = new System.DateTime (2020, 8, 31), ApproximatePremiumAmount = 110, Issuer = "quote_issuer1", ProposalInitiator = "proposer1" },
					new ProposalModel { ProposalId = 5, QuoteId = 5, VehicleType = "Convertible", CarPrice = 9000, FromDate = new System.DateTime (2020, 4, 10), ToDate = new System.DateTime (2020, 9, 9), ApproximatePremiumAmount = 180, Issuer = "quote_issuer1", ProposalInitiator = "proposer1" },
					new ProposalModel { ProposalId = 6, QuoteId = 6, VehicleType = "Hatchback", CarPrice = 3300, FromDate = new System.DateTime (2020, 4, 20), ToDate = new System.DateTime (2020, 9, 19), ApproximatePremiumAmount = 66, Issuer = "quote_issuer1", ProposalInitiator = "proposer1" }
				};

		public static IList<PolicyModel> Policies
			= new List<PolicyModel>
				{
					new PolicyModel { PolicyId = 1, ProposalId = 1, QuoteId = 1, VehicleType = "Hybrid", CarPrice = 5500, FromDate = new System.DateTime (2020, 4, 1), ToDate = new System.DateTime (2020, 8, 31), ApproximatePremiumAmount = 110, Issuer = "quote_issuer1", ProposalInitiator = "policy_approver1", PolicyApprover = "policy_approver1" },
					new PolicyModel { PolicyId = 2, ProposalId = 2, QuoteId = 2, VehicleType = "Convertible", CarPrice = 9000, FromDate = new System.DateTime (2020, 4, 10), ToDate = new System.DateTime (2020, 9, 9), ApproximatePremiumAmount = 180, Issuer = "quote_issuer1", ProposalInitiator = "policy_approver1", PolicyApprover = "policy_approver1" },
					new PolicyModel { PolicyId = 3, ProposalId = 3, QuoteId = 3, VehicleType = "Hatchback", CarPrice = 3300, FromDate = new System.DateTime (2020, 4, 20), ToDate = new System.DateTime (2020, 9, 19), ApproximatePremiumAmount = 66, Issuer = "quote_issuer1", ProposalInitiator = "policy_approver1", PolicyApprover = "policy_approver1" }
				};

		public static IList<string> GetVehicleTypes ()
		{
			return vehicleTypes;
		}

		public static QuoteModel AddQuote (QuoteModel newQuote, string quoteIssuer)
		{
			newQuote.Issuer = quoteIssuer;
			newQuote.QuoteId = (++quotesCounter);
			newQuote.ApproximatePremiumAmount = newQuote.CarPrice / 100 * 2;

			Quotes.Add (newQuote);
			return newQuote;
		}

		public static ProposalModel PromoteQuoteToProposal (QuoteModel quote, string proposalInitiator)
		{
			var quoteToBePromoted = Quotes.FirstOrDefault (q => q.QuoteId == quote.QuoteId);

			if (quoteToBePromoted != null)
			{
				var newProposal
					= new ProposalModel
					{
						ProposalId = (++proposalCounter),
						QuoteId = quoteToBePromoted.QuoteId,
						CarPrice = quoteToBePromoted.CarPrice,
						VehicleType = quoteToBePromoted.VehicleType,
						FromDate = quoteToBePromoted.FromDate,
						ToDate = quoteToBePromoted.ToDate,
						ApproximatePremiumAmount = quoteToBePromoted.ApproximatePremiumAmount,
						Issuer = quoteToBePromoted.Issuer,
						ProposalInitiator = proposalInitiator
					};

				Quotes.Remove (quoteToBePromoted);
				Proposals.Add (newProposal);

				return newProposal;
			}

			return null;
		}

		public static PolicyModel PromoteProposalToPolicy (ProposalModel proposal, string policyApprover)
		{
			var proposalToBePromoted = Proposals.FirstOrDefault (p => p.ProposalId == proposal.ProposalId);

			if (proposalToBePromoted != null)
			{
				var newPolicy
					= new PolicyModel
					{
						PolicyId = (++policyCounter),
						QuoteId = proposalToBePromoted.QuoteId,
						CarPrice = proposalToBePromoted.CarPrice,
						VehicleType = proposalToBePromoted.VehicleType,
						FromDate = proposalToBePromoted.FromDate,
						ToDate = proposalToBePromoted.ToDate,
						ApproximatePremiumAmount = proposalToBePromoted.ApproximatePremiumAmount,
						Issuer = proposalToBePromoted.Issuer,
						ProposalInitiator = policyApprover,
						PolicyApprover = policyApprover
					};

				Proposals.Remove (proposalToBePromoted);
				Policies.Add (newPolicy);

				return newPolicy;
			}

			return null;
		}
	}
}