using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;
using SampleWebApp.UiTests.LoginTests.Background.ElementIDs;
using OOSelenium.Framework.Entities;

namespace SampleWebApp.UiTests.LoginTests.Pages
{
	public sealed class LoginPage
		: WebUiPageBase
	{
		// Application logo.
		public Image ApplicationLogo { get; private set; }

		// Validation summary.
		public ValidationSummary ValidationSummary
		{
			get
			{
				return
					this.FindById<ValidationSummary> (
						LoginPageElementIDs.ID_VALIDATION_SUMMARY,
						(identifier, webElement, webDriver) => new ValidationSummary (webElement, identifier, LocateByWhat.Id, webDriver));
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
			this.ApplicationLogo
				= base.FindById<Image> (
					LoginPageElementIDs.ID_APPLICATION_LOGO,
					(identifier, webElement, webDriver) => new Image (webElement, identifier, LocateByWhat.Id, webDriver));


			// Validation summary is intentionally left out because when the page loads, the
			// DIV tag for validation summary would not be there.

			// User ID.
			this.UserIdLabel
				= base.FindById<Label> (
					LoginPageElementIDs.ID_USER_ID_LABEL,
					(identifier, webElement, webDriver) => new Label (webElement, identifier, LocateByWhat.Id, webDriver));

			this.UserIdField
				= base.FindById<TextField> (
					LoginPageElementIDs.ID_USER_ID_TEXT_FIELD,
					(identifier, webElement, webDriver) => new TextField (webElement, identifier, LocateByWhat.Id, webDriver));

			// Password.
			this.PasswordLabel
				= base.FindById<Label> (
					LoginPageElementIDs.ID_PASSWORD_LABEL,
					(identifier, webElement, webDriver) => new Label (webElement, identifier, LocateByWhat.Id, webDriver));

			this.PasswordField
				= base.FindById<TextField> (
					LoginPageElementIDs.ID_PASSWORD_TEXT_FIELD,
					(identifier, webElement, webDriver) => new TextField (webElement, identifier, LocateByWhat.Id, webDriver));

			// Login button.
			LoginButton
				= base.FindById<Button> (
					LoginPageElementIDs.ID_LOGIN_BUTTON,
					(identifier, webElement, webDriver) => new Button (webElement, identifier, LocateByWhat.Id, webDriver));
		}
	}
}