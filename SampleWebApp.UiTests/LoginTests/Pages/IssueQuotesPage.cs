using OpenQA.Selenium;

using OOSelenium.Framework.WebUIControls;
using OOSelenium.Framework.Entities;
using SampleWebApp.UiTests.LoginTests.Background.ElementIDs;

namespace SampleWebApp.UiTests.LoginTests.Pages
{
	public sealed class IssueQuotesPage
		: LandingPageBase
	{
		public Link SearchQuotesLink { get; private set; }

		public Link IssueANewQuoteLink { get; private set; }

		public IssueQuotesPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			SearchQuotesLink
				= this.FindById<Link> (
					IssueQuotePageElementIds.ID_SEARCH_QUOTES_LINK,
					(identifier, webElement, webDriver) => new Link (webElement, identifier, LocateByWhat.Id, webDriver));

			IssueANewQuoteLink
				= this.FindById<Link> (
					IssueQuotePageElementIds.ID_ISSUE_A_NEW_QUOTE_LINK,
					(identifier, webElement, webDriver) => new Link (webElement, identifier, LocateByWhat.Id, webDriver));
		}
	}
}