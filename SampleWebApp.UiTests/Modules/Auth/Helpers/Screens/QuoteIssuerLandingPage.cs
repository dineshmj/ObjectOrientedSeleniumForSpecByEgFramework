using OpenQA.Selenium;

using OOSelenium.Framework.WebUIControls;

using SampleWebApp.UiTests.Modules.Auth.Helpers.ElementIDs;

namespace SampleWebApp.UiTests.Modules.Auth.Helpers.Screens
{
	public sealed class QuoteIssuerLandingPage
		: LandingPageBase
	{
		public Link SearchQuotesLink { get; private set; }
		public Link IssueANewQuoteLink { get; private set; }

		public QuoteIssuerLandingPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			this.SearchQuotesLink = base.FindLink (IssueQuoteLandingPageElementIds.ID_SEARCH_QUOTES_LINK);
			this.IssueANewQuoteLink = base.FindLink (IssueQuoteLandingPageElementIds.ID_ISSUE_A_NEW_QUOTE_LINK);
		}
	}
}