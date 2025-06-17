using OOSelenium.Framework.Abstractions;

using GitHubServerSearch.Pages;

namespace GitHubServerSearch.Navigation
{
	public sealed class GitHubNavigationComponent<UserRole, ExecutionEnvironment>
		: WebUiNavigationComponentBase<UserRole, ExecutionEnvironment>
			where UserRole : Enum
			where ExecutionEnvironment : Enum
	{
		public GitHubNavigationComponent (
				IExecutionEnvironmentPageDataProvider<UserRole, ExecutionEnvironment> pageDataProvider,
				IDecryptor decryptor
			)
				: base (pageDataProvider, decryptor)
		{
		}

		public GitHubLoginPage GoToGitHubLoginPage ()
		{
			var provider = base.ExecutionEnvironmentPageDataProvider;

			return
				new GitHubLoginPage (
					base.WebDriver,
					provider.GetWebApplicationUrlFor (base.ExecutionEnvironment)
				);
		}

		public GitHubSsoConfirmationPage LoginAndGetSsoConfirmationPage (string encryptedUsername, string encryptedPassword)
		{
			var loginPage = this.GoToGitHubLoginPage ();
			loginPage.Login (encryptedUsername, encryptedPassword);

			//
			// Now that logging in is complete, the screen will navigate to the SSO confirmation page.
			// Prepare an instance of the SSO confirmation page from the current screen in the browser.
			//

			var ssoConfirmationPage = base.GetPageInstance<GitHubSsoConfirmationPage> ();
			return ssoConfirmationPage;
		}

		public GitHubHomePage LoginConfirmSsoAndGoToGitHubHomePage (string encryptedUsername, string encryptedPassword)
		{
			var ssoConfirmationPage
				= this.LoginAndGetSsoConfirmationPage (encryptedUsername, encryptedPassword);

			ssoConfirmationPage.ConfirmSso ();

			//
			// Now that SSO confirmation is complete, the screen will navigate to the GitHub home page.
			// Prepare an instance of the GitHub home page from the current screen in the browser.
			//

			var homePage = base.GetPageInstance<GitHubHomePage> ();
			return homePage;
		}
	}
}