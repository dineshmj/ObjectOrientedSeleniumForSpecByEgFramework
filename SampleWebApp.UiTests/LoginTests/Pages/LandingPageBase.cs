using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;
using SampleWebApp.UiTests.LoginTests.Background.ElementIDs;

namespace SampleWebApp.UiTests.LoginTests.Pages
{
	public abstract class LandingPageBase
		: WebUiPageBase
	{
		public Link HomeLink { get; private set; }

		public Link LogoutLink { get; private set; }

		public LandingPageBase (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			HomeLink = FindLinkById (IssueQuotePageElementIds.ID_INS_ONE_HOME_LINK);
			LogoutLink = FindLinkById (IssueQuotePageElementIds.ID_LOGOUT_LINK);
		}
	}
}