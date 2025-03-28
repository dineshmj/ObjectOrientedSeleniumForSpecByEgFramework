using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using GitHubServerSearch.Background;
using OOSelenium.Framework.Extensions;

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
			this.ConfirmSsoButton = base.FindButtonByCss (CssClasses.CSS_SSO_CONFIRMATION_SUBMIT_BUTTON.RefineForButton ());
		}

		public void ConfirmSso ()
		{
			this.ConfirmSsoButton.Click ();
		}
	}
}