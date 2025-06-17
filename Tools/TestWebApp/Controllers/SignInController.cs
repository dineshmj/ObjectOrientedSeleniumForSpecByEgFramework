using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

using OOSelenium.TestWebApp.Business;
using OOSelenium.TestWebApp.Business.Entities;
using OOSelenium.TestWebApp.Models;

namespace OOSelenium.TestWebApp.Controllers
{
	public sealed class SignInController
		: Controller
	{
		[HttpGet]
		public ActionResult SignIn ()
		{
			var emptySignInCredentials = new SignInModel ();
			return base.View (emptySignInCredentials);
		}

		[HttpPost]
		public ActionResult SignIn (SignInModel credentials)
		{
			var signingInUser = InsuranceOneBizManager.Users.FirstOrDefault (u => u.UserId == credentials.UserId && u.EncryptedPassword == credentials.EncryptedPassword);

			if (signingInUser == null)
			{
				base.ModelState.AddModelError (String.Empty, "Invalid user ID or password.");
				return base.View ();
			}

			// Authenticate and have the user signed in.
			FormsAuthentication.SetAuthCookie (signingInUser.UserId, false);
			base.Session [SessionKeys.SIGNED_IN_USER_KEY] = signingInUser;
			return base.RedirectToAction ("LandingPage", "Master");
		}

		public ActionResult SignOut ()
		{
			base.Session [SessionKeys.SIGNED_IN_USER_KEY] = null;
			FormsAuthentication.SignOut ();
			return base.RedirectToAction ("SignIn");
		}
	}
}