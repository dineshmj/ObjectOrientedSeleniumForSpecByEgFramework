using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using GitHubServerSearch.Background;
using OOSelenium.Framework.Extensions;
using OOSelenium.Framework.Entities;

namespace GitHubServerSearch.Pages
{
	public sealed class GitHubSsoConfirmationPage
		: WebUiPageBase
	{
		public Button ConfirmSsoButton { get; init; }

		public GitHubSsoConfirmationPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			Thread.Sleep (3000);
			this.ConfirmSsoButton
				= this.FindByCssClass<Button> (
					CssClasses.CSS_SSO_CONFIRMATION_SUBMIT_BUTTON.RefineForButton (),
					(identifier, webElement, webDriver) => new Button (webElement, identifier, LocateByWhat.Id, webDriver));
		}

		public void ConfirmSso ()
		{
			this.ConfirmSsoButton.Click ();
		}
	}
}