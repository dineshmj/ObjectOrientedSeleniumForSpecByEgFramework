using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OOSelenium.TestWebApp.Business.Entities;
using OOSelenium.TestWebApp.Models;

namespace OOSelenium.TestWebApp.Controllers
{
	public sealed class MasterController
		: Controller
	{
		[Authorize]
		public ActionResult LandingPage ()
		{
			var signedInUser = (SignInModel) base.Session [SessionKeys.SIGNED_IN_USER_KEY];
			var linkDictionary = new Dictionary<string, KeyValuePair<string, string>> ();

			switch (signedInUser.UserRole)
			{
				case UserRole.Admin:
					linkDictionary.Add ("Search users", new KeyValuePair<string, string> ("Search", "User"));
					linkDictionary.Add ("Create a new user", new KeyValuePair<string, string> ("Create", "User"));
					break;

				case UserRole.QuoteIssuer:
					linkDictionary.Add ("Search quotes", new KeyValuePair<string, string> ("List", "Quote"));
					linkDictionary.Add ("Issue a new quote", new KeyValuePair<string, string> ("Issue", "Quote"));
					break;

				case UserRole.ProposalInitiator:
					linkDictionary.Add ("Convert a quote to proposal", new KeyValuePair<string, string> ("Promote", "Proposal"));
					break;

				case UserRole.PolicyApprover:
					linkDictionary.Add ("Issue policy from Proposal", new KeyValuePair<string, string> ("Promote", "Policy"));
					break;

				default:
					throw new NotImplementedException ($"Role of the user with ID \"{ signedInUser.UserId }\" was not recognized.");
			}

			base.ViewBag.MenuLinks = linkDictionary;

			return View ();
		}
	}
}