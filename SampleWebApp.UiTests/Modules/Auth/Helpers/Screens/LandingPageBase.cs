using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using SampleWebApp.UiTests.Modules.Auth.Helpers.ElementIDs;

namespace SampleWebApp.UiTests.Modules.Auth.Helpers.Screens
{
	public abstract class LandingPageBase
		: WebUiPageBase
	{
		public Link HomeLink { get; private set; }
		public Link SignOutLink { get; private set; }

		public LandingPageBase (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			this.HomeLink = base.FindLink (IssueQuoteLandingPageElementIds.ID_INS_ONE_HOME_LINK);
			this.SignOutLink = base.FindLink (IssueQuoteLandingPageElementIds.ID_SIGN_OUT_LINK);
		}
	}
}