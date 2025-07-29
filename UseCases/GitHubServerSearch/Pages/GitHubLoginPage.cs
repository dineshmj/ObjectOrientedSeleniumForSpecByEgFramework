using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.WebUIControls;

using SimpleQA.Framework.Selenium.Abstractions;
using SimpleQA.UseCases.Selenium.GitHubServerSearch.Background;

namespace SimpleQA.UseCases.Selenium.GitHubServerSearch.Pages
{
	public sealed class GitHubLoginPage
		: SeleniumPageBase
	{
		public TextField UsernameTextField { get; init; }

		public TextField PasswordTextField { get; init; }

		public Button LoginButton { get; init; }

		public GitHubLoginPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			this.UsernameTextField
				= this.FindById<TextField> (
					ElementIds.ID_LOGIN_ID_FIELD,
					(identifier, webElement, webDriver) => new TextField (webElement, identifier, LocateByWhat.Id, webDriver));

			this.PasswordTextField
				= this.FindById<TextField> (
					ElementIds.ID_LOGIN_PASSWORD_FIELD,
					(identifier, webElement, webDriver) => new TextField (webElement, identifier, LocateByWhat.Id, webDriver));

			this.LoginButton
				= this.FindByName<Button> (
					ElementNames.NAME_LOGIN_BUTTON,
					(identifier, webElement, webDriver) => new Button (webElement, identifier, LocateByWhat.Id, webDriver));
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