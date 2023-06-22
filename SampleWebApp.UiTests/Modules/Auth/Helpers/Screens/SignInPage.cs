using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using SampleWebApp.UiTests.Modules.Auth.Helpers.ElementIDs;

namespace SampleWebApp.UiTests.Modules.Auth.Helpers.Screens
{
	public sealed class SignInPage
		: WebUiPageBase
	{
		// Application logo.
		public Picture ApplicationLogo { get; private set; }

		// Validation summary.
		public ValidationSummary ValidationSummary
		{
			get
			{
				return base.FindValidationSummary (SignInPageElementIDs.ID_VALIDATION_SUMMARY);
			}
		}

		// User ID.
		public Label UserIdLabel { get; private set; }

		public TextField UserIdField { get; private set; }

		// Password.
		public Label PasswordLabel { get; private set; }

		public TextField PasswordField { get; private set; }

		// Sign in button.
		public Button SignInButton { get; private set; }

		public SignInPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			// Application logo.
			this.ApplicationLogo = base.FindImage (SignInPageElementIDs.ID_APPLICATION_LOGO);

			// Validation summary is intentionally left out because when the page loads, the
			// DIV tag for validation summary would not be there.

			// User ID.
			this.UserIdLabel = base.FindLabel (SignInPageElementIDs.ID_USER_ID_LABEL);
			this.UserIdField = base.FindTextField (SignInPageElementIDs.ID_USER_ID_TEXT_FIELD);

			// Password.
			this.PasswordLabel = base.FindLabel (SignInPageElementIDs.ID_PASSWORD_LABEL);
			this.PasswordField = base.FindTextField (SignInPageElementIDs.ID_PASSWORD_TEXT_FIELD);

			// Sign in button.
			this.SignInButton = base.FindButton (SignInPageElementIDs.ID_SIGN_IN_BUTTON);
		}
	}
}