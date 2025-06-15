using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;
using OOSelenium.Framework.Entities;
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
			HomeLink
				= this.FindById<Link> (
					IssueQuotePageElementIds.ID_INS_ONE_HOME_LINK,
					(identifier, webElement, webDriver) => new Link (webElement, identifier, LocateByWhat.Id, webDriver));

			LogoutLink
				= this.FindById<Link> (
					IssueQuotePageElementIds.ID_LOGOUT_LINK,
					(identifier, webElement, webDriver) => new Link (webElement, identifier, LocateByWhat.Id, webDriver));
		}
	}
}