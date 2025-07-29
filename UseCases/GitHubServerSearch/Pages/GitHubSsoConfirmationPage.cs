using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;
using SimpleQA.Framework.Selenium.Extensions;
using SimpleQA.Framework.Selenium.WebUIControls;
using SimpleQA.UseCases.Selenium.GitHubServerSearch.Background;

namespace SimpleQA.UseCases.Selenium.GitHubServerSearch.Pages
{
	public sealed class GitHubSsoConfirmationPage
		: SeleniumPageBase
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