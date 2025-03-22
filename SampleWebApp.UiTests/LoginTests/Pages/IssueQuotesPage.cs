using OpenQA.Selenium;

using OOSelenium.Framework.WebUIControls;
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
			SearchQuotesLink = FindLink (IssueQuotePageElementIds.ID_SEARCH_QUOTES_LINK);
			IssueANewQuoteLink = FindLink (IssueQuotePageElementIds.ID_ISSUE_A_NEW_QUOTE_LINK);
		}
	}
}