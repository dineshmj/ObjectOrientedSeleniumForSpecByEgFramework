using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using GitHubServerSearch.Background;

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
			this.ConfirmSsoButton = base.FindButtonByCss (CssClasses.CSS_SSO_CONFIRMATION_SUBMIT_BUTTON);
		}

		public void ConfirmSso ()
		{
			this.ConfirmSsoButton.Click ();
		}
	}
}