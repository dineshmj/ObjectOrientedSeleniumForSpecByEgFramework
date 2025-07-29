using FluentAssertions;
using Xbehave;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;
using OOSelenium.WebUiTests.TestWebAppTests.Background;
using OOSelenium.WebUiTests.TestWebAppTests.Navigation;
using OOSelenium.WebUiTests.TestWebAppTests.Pages;
using OOSelenium.WebUiTests.TestWebAppTests.Background.ElementIDs;
using OOSelenium.WebUiTests.TestWebAppTests.DataProviders;

namespace OOSelenium.WebUiTests.TestWebAppTests.Tests
{
	public sealed class InsuranceOneLoginTests
		: WebUiTestBase
	{
		private readonly IExecutionEnvironmentPageDataProvider<UserRole, ExecutionEnvironment> dataProvider;
		private readonly IDecryptor decryptor;
		private readonly LoginNavigationComponent<UserRole, ExecutionEnvironment> loginNavigationComponent;
		private InsuranceOneLoginPage loginPage;

		public InsuranceOneLoginTests ()
		{
			this.dataProvider = new InsuranceOneTestDataProvider ();
			this.decryptor = new PassThroughDecryptor ();
			this.loginNavigationComponent
				= new LoginNavigationComponent<UserRole, ExecutionEnvironment>
					(
						dataProvider,
						decryptor
					);
		}

		[Scenario]
		public async void LoginPage_Title_And_LabelTexts_MustBe_Correct ()
		{
			var userIdLabel = string.Empty; var passwordLabel = string.Empty; var loginButtonText = string.Empty;
			var pageTitle = string.Empty;
			var userIdFieldCssClass = string.Empty; var userIdFieldPlaceholder = string.Empty;
			var passwordFieldCssClass = string.Empty; var passwordFieldPlaceholder = string.Empty;
			var isPasswordFieldObscured = false; var logoPicture = (Image) null;

			"Given that Four Walls Inc. Insurance One Login page is accessible"
				.x (() =>
				{
					loginPage = loginNavigationComponent.GoToInsuranceOneLoginPage ();
				});

			"When I check the page title, labels of UI fileds, and texts of buttons"
				.x (() =>
				{
					// Logo.
					logoPicture = loginPage.ApplicationLogoImage;

					// Credentials fields.
					var userIdField = loginPage.UserIdTextField;
					var passwordField = loginPage.PasswordTextField;

					// Read the UI field labels.
					pageTitle = loginPage.Title;
					userIdLabel = loginPage.UserIdLabel.Text;
					passwordLabel = loginPage.PasswordLabel.Text;
					loginButtonText = loginPage.SignInSubmitButton.Text;

					// User ID field.
					userIdFieldCssClass = userIdField.CssClass;
					userIdFieldPlaceholder = userIdField.PlaceHolderText;

					// Password field.
					passwordFieldCssClass = passwordField.CssClass;
					passwordFieldPlaceholder = passwordField.PlaceHolderText;
					isPasswordFieldObscured = passwordField.IsPassword;
				});

			"Then the page title, labels and texts, CSS classes used, etc. must be as expected"
				.x (() =>
				{
					// Assert expectations.

					// Logo.
					var logoSize = logoPicture.ImageBitmap.Size;
					logoSize.Width.Should ().Be (250);
					logoSize.Height.Should ().Be (169);

					// Labels.
					pageTitle.Should ().Be (Expectations.LOGIN_PAGE_TITLE);
					userIdLabel.Should ().Be (Expectations.LOGIN_PAGE_USER_ID_FIELD_LABEL_TEXT);
					passwordLabel.Should ().Be (Expectations.LOGIN_PAGE_PASSWORD_FIELD_LABEL_TEXT);
					loginButtonText.Should ().Be (Expectations.LOGIN_PAGE_LOGIN_BUTTON_TEXT);

					// CSS classes.
					userIdFieldCssClass.Should ().Contain (CssClassNames.REGULAR_TEXT_FIELD_CSS_CLASS);
					passwordFieldCssClass.Should ().Contain (CssClassNames.PASSWORD_FIELD_CSS_CLASS);

					// Placeholders.
					userIdFieldPlaceholder.Should ().Be (Expectations.LOGIN_PAGE_USER_ID_FIELD_PLACEHOLDER);
					passwordFieldPlaceholder.Should ().Be (Expectations.LOGIN_PAGE_PASSWORD_FIELD_PLACEHOLDER);

					// Others.
					isPasswordFieldObscured.Should ().BeTrue ();
				});
		}
		
		[Scenario]
		public void When_WrongCredentials_Entered_ValidationFailureMessages_AreShown ()
		{
			IList<string> validationSummaryMessages = null;

			"Given that Four Walls Inc. Insurance One login page is accessible"
				.x (() =>
				{
					// Go to the login page.
					loginPage = loginNavigationComponent.GoToInsuranceOneLoginPage ();
				});

			"When enter invalid credentials on the sign in page and click the \"Sign in\" button"
				.x (() =>
				{
					// Enter invalid credentials.
					loginPage.UserIdTextField.SetText ("pqr");
					loginPage.PasswordTextField.SetText ("xyz");

					// Click "sign in" button.
					loginPage.SignInSubmitButton.Click ();
				});

			"Then I should see relevant validation failure messages related to failed logging in attempt."
			.x (() =>
				{
					// Get the validation summary error messages.
					validationSummaryMessages = loginPage.ValidationSummary.ValidationFailureMessages;

					// Assert the expectations.
					validationSummaryMessages.Should ().NotBeNull ();
					validationSummaryMessages [0].Should ().Be (Expectations.INVALID_USER_ID_OR_PASSWORD_MESSAGE);
				});
		}

		public override void Dispose ()
		{
			loginNavigationComponent.Dispose ();
			loginPage.Dispose ();
		}
	}
}