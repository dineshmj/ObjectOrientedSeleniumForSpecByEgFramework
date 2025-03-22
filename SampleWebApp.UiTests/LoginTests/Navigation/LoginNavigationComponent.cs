using OOSelenium.Framework.Abstractions;
using SampleWebApp.UiTests.LoginTests.Pages;

namespace SampleWebApp.UiTests.LoginTests.Navigation
{
	public sealed class LoginNavigationComponent<UserRole, ExecutionEnvironment>
		: BusinessNavigationComponentBase<UserRole, ExecutionEnvironment>
	{
		// Constructor.
		public LoginNavigationComponent
			(
				IExecutionEnvironmentDataProvider<UserRole, ExecutionEnvironment> testBackgroundDataProvider,
				IDecryptor decryptor
			)
			: base (testBackgroundDataProvider, decryptor)
		{
		}

		// Public methods.
		public LoginPage GoToLoginPage ()
		{
			var provider = ExecutionEnvironmentDataProvider;

			return
				new LoginPage
				(
					WebDriver,
					provider.GetApplicationBaseUrlFor (base.ExecutionEnvironment)
				);
		}

		public TPage LoginAndGoToPage<TPage> (UserRole userRole)
			where TPage : WebUiPageBase
		{
			// Sign in to the application with a credentials, whose Role is the specified one.
			LoginToApplication (userRole);
			
			//
			// Now that logging in is complete, the screen will navigate to the appropriate page.
			// Prepare an instance of the page to which the screen will navigate, if a user with specified role logs in.
			// The constructor of the page will look for specific UI fields in that page.
			// E.g.: Issue Quote page, etc.
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
			var credDictionary = ExecutionEnvironmentDataProvider.GetCredentialsFor (base.ExecutionEnvironment);
			var credential = credDictionary [userRole];

			// Go to sign in page.
			var loginPage = GoToLoginPage ();

			// Enter credentials and sign in.
			loginPage.UserIdField.SetText (credential.UserId);
			loginPage.PasswordField.SetText (Decryptor.Decrypt (credential.EncryptedPassword));

			// Click on sign-in button.
			loginPage.LoginButton.Click ();
		}
	}
}