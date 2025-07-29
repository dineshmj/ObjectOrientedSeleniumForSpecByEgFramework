using SimpleQA.Framework.Abstractions;
using SimpleQA.Framework.Selenium.Abstractions;
using SimpleQA.UseCases.Selenium.WebUITests.TestWebAppTests.Pages;

namespace SimpleQA.UseCases.Selenium.WebUITests.TestWebAppTests.Navigation
{
	public sealed class LoginNavigationComponent<UserRole, ExecutionEnvironment>
		: SeleniumNavigationComponentBase<UserRole, ExecutionEnvironment>
		where UserRole : Enum
		where ExecutionEnvironment : Enum
	{
		// Constructor.
		public LoginNavigationComponent
			(
				IEnvironmentDataProvider<UserRole, ExecutionEnvironment> testBackgroundDataProvider,
				IDecryptor decryptor
			)
			: base (testBackgroundDataProvider, decryptor)
		{
		}

		public InsuranceOneLoginPage GoToInsuranceOneLoginPage ()
		{
			return
				new InsuranceOneLoginPage (
					this.WebDriver,
					this.EnvironmentDataProvider.GetApplicationUrlFor(base.ExecutionEnvironment),
					navigationRequired: true,
					maximizeWindow: true);
		}

		// Public methods.
		public LoginPage GoToLoginPage ()
		{
			var provider = EnvironmentDataProvider;

			return
				new LoginPage (
					WebDriver,
					provider.GetApplicationUrlFor (base.ExecutionEnvironment),
					navigationRequired: true,
					maximizeWindow: true);
		}

		public TPage LoginAndGoToPage<TPage> (UserRole userRole)
			where TPage : SeleniumPageBase
		{
			// Sign in to the application with a credentials, whose Role is the specified one.
			this.LoginToApplication (userRole);
			
			//
			// Now that logging in is complete, the screen will navigate to the appropriate page.
			// Prepare an instance of the page to which the screen will navigate, if a user with specified role logs in.
			// The constructor of the page will look for specific UI fields in that page.
			// E.g.: Issue Quote page, etc.
			//
			return
				(TPage) Activator.CreateInstance
					(
						typeof (TPage),
						[ WebDriver, WebDriver.Url ]
					);
		}

		// Private methods.
		private void LoginToApplication (UserRole userRole)
		{
			// Get credentials for the role.
			var credDictionary = this.EnvironmentDataProvider.GetCredentialsFor (base.ExecutionEnvironment);
			var credential = credDictionary [userRole];

			// Go to sign in page.
			var loginPage = this.GoToLoginPage ();

			// Enter credentials and sign in.
			loginPage.UserIdField.SetText (credential.UserId);
			loginPage.PasswordField.SetText (this.Decryptor.Decrypt (credential.EncryptedPassword));

			// Click on sign-in button.
			loginPage.LoginButton.Click ();
		}
	}
}