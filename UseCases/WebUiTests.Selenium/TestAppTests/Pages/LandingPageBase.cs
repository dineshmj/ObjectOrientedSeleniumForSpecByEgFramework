using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;
using SimpleQA.Framework.Selenium.WebUIControls;
using SimpleQA.UseCases.Selenium.WebUITests.TestWebAppTests.Background.ElementIDs;

namespace SimpleQA.UseCases.Selenium.WebUITests.TestWebAppTests.Pages
{
	public abstract class LandingPageBase
		: SeleniumPageBase
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