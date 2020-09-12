using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using OpenQA.Selenium;

using SampleWebApp.UiTests.Modules.Auth.Helpers.ElementIDs;

namespace SampleWebApp.UiTests.Modules.Auth.Helpers.Screens
{
	public sealed class QuoteIssuerLandingPage
		: WebUiPageBase
	{
		public Link HomeLink { get; private set; }
		public Link SignOutLink { get; private set; }
		public Link SearchQuotesLink { get; private set; }
		public Link IssueANewQuoteLink { get; private set; }

		public QuoteIssuerLandingPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			this.HomeLink = base.FindLink (IssueQuoteLandingPageElementIds.ID_INS_ONE_HOME_LINK);
			this.SignOutLink = base.FindLink (IssueQuoteLandingPageElementIds.ID_SIGN_OUT_LINK);
			this.SearchQuotesLink = base.FindLink (IssueQuoteLandingPageElementIds.ID_SEARCH_QUOTES_LINK);
			this.IssueANewQuoteLink = base.FindLink (IssueQuoteLandingPageElementIds.ID_ISSUE_A_NEW_QUOTE_LINK);
		}
	}
}
