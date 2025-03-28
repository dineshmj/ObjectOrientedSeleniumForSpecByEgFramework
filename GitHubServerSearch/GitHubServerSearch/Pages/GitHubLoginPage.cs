using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using GitHubServerSearch.Background;

namespace GitHubServerSearch.Pages
{
	public sealed class GitHubLoginPage
		: WebUiPageBase
	{
		public TextField UsernameTextField { get; init; }

		public TextField PasswordTextField { get; init; }

		public Button LoginButton { get; init; }

		public GitHubLoginPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			this.UsernameTextField = this.FindTextFieldById (ElementIds.ID_LOGIN_ID_FIELD);
			this.PasswordTextField = this.FindTextFieldById (ElementIds.ID_LOGIN_PASSWORD_FIELD);
			this.LoginButton = this.FindButtonByName (ElementNames.NAME_LOGIN_BUTTON);
		}

		public void Login (string username, string password)
		{
			this.UsernameTextField.TypeEachCharacter (username);
			this.PasswordTextField.SetText (password);

			Thread.Sleep (3000);
			this.LoginButton.Click ();
		}
	}
}