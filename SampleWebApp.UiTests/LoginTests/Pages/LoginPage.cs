using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;
using SampleWebApp.UiTests.LoginTests.Background.ElementIDs;

namespace SampleWebApp.UiTests.LoginTests.Pages
{
	public sealed class LoginPage
		: WebUiPageBase
	{
		// Application logo.
		public Picture ApplicationLogo { get; private set; }

		// Validation summary.
		public ValidationSummary ValidationSummary
		{
			get
			{
				return FindValidationSummary (LoginPageElementIDs.ID_VALIDATION_SUMMARY);
			}
		}

		// User ID.
		public Label UserIdLabel { get; private set; }

		public TextField UserIdField { get; private set; }

		// Password.
		public Label PasswordLabel { get; private set; }

		public TextField PasswordField { get; private set; }

		// Sign in button.
		public Button LoginButton { get; private set; }

		public LoginPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			// Application logo.
			ApplicationLogo = FindImage (LoginPageElementIDs.ID_APPLICATION_LOGO);

			// Validation summary is intentionally left out because when the page loads, the
			// DIV tag for validation summary would not be there.

			// User ID.
			UserIdLabel = FindLabel (LoginPageElementIDs.ID_USER_ID_LABEL);
			UserIdField = FindTextField (LoginPageElementIDs.ID_USER_ID_TEXT_FIELD);

			// Password.
			PasswordLabel = FindLabel (LoginPageElementIDs.ID_PASSWORD_LABEL);
			PasswordField = FindTextField (LoginPageElementIDs.ID_PASSWORD_TEXT_FIELD);

			// Login button.
			LoginButton = FindButton (LoginPageElementIDs.ID_LOGIN_BUTTON);
		}
	}
}