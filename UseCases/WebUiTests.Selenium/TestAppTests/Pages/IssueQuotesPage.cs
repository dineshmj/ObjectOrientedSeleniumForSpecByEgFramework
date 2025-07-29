using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.WebUIControls;
using SimpleQA.UseCases.Selenium.WebUITests.TestWebAppTests.Background.ElementIDs;

namespace SimpleQA.UseCases.Selenium.WebUITests.TestWebAppTests.Pages
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