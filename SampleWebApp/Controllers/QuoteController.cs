using System;
using System.Linq;
using System.Web.Mvc;

using OOSelenium.TestWebApp.Business;
using OOSelenium.TestWebApp.Business.Entities;
using OOSelenium.TestWebApp.Models;

namespace OOSelenium.TestWebApp.Controllers
{
	public sealed class QuoteController
		: Controller
	{
		[HttpGet]
		public ActionResult Issue ()
		{
			base.ViewBag.VehicleTypes
				= InsuranceOneBizManager
					.GetVehicleTypes ()
					.Select
						(
							vt =>
							{
								return new SelectListItem { Text = vt, Value = vt };
							}
						)
					.ToArray ();

			var newQuote
				= new QuoteModel
					{
						FromDate = DateTime.Today,
						ToDate = DateTime.Today.AddMonths (6)
					};

			return base.View (newQuote);
		}

		[HttpPost]
		public ActionResult Issue (QuoteModel newQuote)
		{
			if (newQuote.CarPrice == 0)
			{
				base.ViewBag.VehicleTypes
				= InsuranceOneBizManager
					.GetVehicleTypes ()
					.Select
						(
							vt =>
							{
								return new SelectListItem { Text = vt, Value = vt };
							}
						)
					.ToArray ();

				base.ModelState.AddModelError ("CarPrice", "Car price cannot be less than $ 1000.");
				return base.View ();
			}


			var signedInUser = (SignInModel) base.Session [SessionKeys.SIGNED_IN_USER_KEY];
			var issuedQuote = InsuranceOneBizManager.AddQuote (newQuote, signedInUser.UserId);

			return base.RedirectToAction ("Show", new { quoteId = issuedQuote.QuoteId });
		}

		[HttpGet]
		public ActionResult Show (int quoteId)
		{
			var quoteToBeShown = InsuranceOneBizManager.Quotes.FirstOrDefault (q => q.QuoteId == quoteId);
			return base.View (quoteToBeShown);
		}

		[HttpGet]
		public ActionResult List ()
		{
			var allQuotes = InsuranceOneBizManager.Quotes;
			return base.View (allQuotes);
		}
	}
}