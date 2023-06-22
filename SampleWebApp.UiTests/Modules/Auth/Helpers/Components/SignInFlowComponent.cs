using OOSelenium.Framework.Abstractions;

using SampleWebApp.UiTests.Modules.Auth.Helpers.Screens;

namespace SampleWebApp.UiTests.Modules.Auth.Helpers.Components
{
	public sealed class SignInFlowComponent<UserRole, TestEnvironment>
		: BusinessFunctionFlowComponentBase<UserRole, TestEnvironment>
	{
		// Constructor.
		public SignInFlowComponent
			(
				ITestBackgroundDataProvider<UserRole, TestEnvironment> testBackgroundDataProvider,
				IDecryptor decryptor
			)
			: base (testBackgroundDataProvider, decryptor)
		{
		}

		// Public methods.
		public SignInPage GoToSignInPage ()
		{
			var provider = base.TestBackgroundDataProvider;

			return
				new SignInPage
				(
					base.WebDriver,
					provider.GetTargetApplicationBaseUrlFor (base.TestEnvironment)
				);
		}

		public TPage GetLandingPageFor<TPage> (UserRole userRole)
			where TPage : WebUiPageBase
		{
			// Sign in to the application.
			this.SignInWithCredentialsFor (userRole);

			// Prepare the landing page for the specified user role.
			return
				(TPage) Activator.CreateInstance
					(
						typeof (TPage),
						new object [] { this.WebDriver, this.WebDriver.Url}
					);
		}

		// Private methods.
		private void SignInWithCredentialsFor (UserRole userRole)
		{
			// Get credentials for the role.
			var credDictionary = this.TestBackgroundDataProvider.GetValidCredentialsOfUserTypesFor (base.TestEnvironment);
			var signingInUser = credDictionary [userRole];

			// Go to sign in page.
			var signInPage = this.GoToSignInPage ();

			// Enter credentials and sign in.
			signInPage.UserIdField.SetText (signingInUser.UserId);
			signInPage.PasswordField.SetText (base.Decryptor.Decrypt (signingInUser.EncryptedPassword));
			signInPage.SignInButton.Click ();
		}
	}
}