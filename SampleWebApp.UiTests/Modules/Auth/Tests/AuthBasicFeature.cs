using System;
using System.Collections.Generic;

using FluentAssertions;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using SampleWebApp.UiTests.Entities;
using SampleWebApp.UiTests.Modules.Auth.Helpers;
using SampleWebApp.UiTests.Modules.Auth.Helpers.Components;
using SampleWebApp.UiTests.Modules.Auth.Helpers.ElementIDs;
using SampleWebApp.UiTests.Modules.Auth.Helpers.Screens;
using SampleWebApp.UiTests.Preparatory;

using Xbehave;

namespace SampleWebApp.UiTests.Modules.Auth.Tests
{
	public sealed class AuthBasicFeature
		: WebUiTestBase
	{
		private readonly ITestBackgroundDataProvider<UserRole, TestEnvironment> dataProvider;
		private readonly IDecryptor decryptor;
		private readonly SignInFlowComponent<UserRole, TestEnvironment> signInComponent;
		private SignInPage signInPage;

		public AuthBasicFeature ()
		{
			this.dataProvider = new InsuranceOneTestBackgroundDataProvider ();
			this.decryptor = new DummyDecryptor ();
			this.signInComponent
				= new SignInFlowComponent<UserRole, TestEnvironment>
					(
						this.dataProvider,
						this.decryptor
					);
		}

		[Scenario]
		public async void Sign_in_page_must_have_its_title_and_UI_controls_with_correct_labels_and_texts ()
		{
			var userIdLabel = String.Empty; var passwordLabel = String.Empty; var signInButtonText = String.Empty; var pageTitle = String.Empty;
			var userIdFieldCssClass = String.Empty; var userIdFieldPlaceholder = String.Empty;
			var passwordFieldCssClass = String.Empty; var passwordFieldPlaceholder = String.Empty;
			var isPasswordFieldObscured = false; var logoPicture = (Picture) null;

			"Given that Four Walls Inc. Insurance One sign in page is accessible"
				.x (() =>
				{
					this.signInPage = this.signInComponent.GoToSignInPage ();
				});

			"When I check the page title, labels of UI fileds, and text of buttons"
				.x (() =>
				{
					// Logo.
					logoPicture = this.signInPage.ApplicationLogo;

					// Credentials fields.
					var userIdField = this.signInPage.UserIdField;
					var passwordField = this.signInPage.PasswordField;

					// Read the UI field labels.
					pageTitle = this.signInPage.Title;
					userIdLabel = this.signInPage.UserIdLabel.Text;
					passwordLabel = this.signInPage.PasswordLabel.Text;
					signInButtonText = this.signInPage.SignInButton.Text;

					// User ID field.
					userIdFieldCssClass = userIdField.CssClass;
					userIdFieldPlaceholder = userIdField.PlaceHolderText;

					// Password field.
					passwordFieldCssClass = passwordField.CssClass;
					passwordFieldPlaceholder = passwordField.PlaceHolderText;
					isPasswordFieldObscured = passwordField.IsPassword;
				});

			"Then the page title, labels and texts, CSS classes should be as expected"
				.x (() =>
				{
					// Assert expectations.

					// Logo.
					var logoSize = logoPicture.ImageBitmap.Size;
					logoSize.Width.Should ().Be (250);
					logoSize.Height.Should ().Be (169);

					// Labels.
					pageTitle.Should ().Be (Expectations.SIGN_IN_PAGE_TITLE);
					userIdLabel.Should ().Be (Expectations.SIGN_IN_PAGE_USER_ID_FIELD_LABEL_TEXT);
					passwordLabel.Should ().Be (Expectations.SIGN_IN_PAGE_PASSWORD_FIELD_LABEL_TEXT);
					signInButtonText.Should ().Be (Expectations.SIGN_IN_PAGE_SIGN_IN_BUTTON_TEXT);

					// CSS classes.
					userIdFieldCssClass.Should ().Contain (CssClassNames.REGULAR_TEXT_FIELD_CSS_CLASS);
					passwordFieldCssClass.Should ().Contain (CssClassNames.PASSWORD_FIELD_CSS_CLASS);

					// Placeholders.
					userIdFieldPlaceholder.Should ().Be (Expectations.SIGN_IN_PAGE_USER_ID_FIELD_PLACEHOLDER);
					passwordFieldPlaceholder.Should ().Be (Expectations.SIGN_IN_PAGE_PASSWORD_FIELD_PLACEHOLDER);

					// Others.
					isPasswordFieldObscured.Should ().BeTrue ();
				});
		}
		
		[Scenario]
		public void Validation_failure_messages_should_be_displayed_upon_entering_invalid_credentials ()
		{
			IList<string> validationSummaryMessages = null;

			"Given that Four Walls Inc. Insurance One sign in page is accessible"
				.x (() =>
				{
					// Go to the sign in page.
					this.signInPage = this.signInComponent.GoToSignInPage ();
				});

			"When enter invalid credentials on the sign in page and click the \"Sign in\" button"
				.x (() =>
				{
					// Enter invalid credentials.
					this.signInPage.UserIdField.SetText ("pqr");
					this.signInPage.PasswordField.SetText ("xyz");

					// Click "sign in" button.
					this.signInPage.SignInButton.Click ();
				});

			"Then I should see relevant validation failure messages related to failed signing in attempt."
			.x (() =>
				{
					// Get the validation summary error messages.
					validationSummaryMessages = this.signInPage.ValidationSummary.ValidationFailureMessages;

					// Assert the expectations.
					validationSummaryMessages.Should ().NotBeNull ();
					validationSummaryMessages [0].Should ().Be (Expectations.INVALID_USER_ID_OR_PASSWORD_MESSAGE);
				});
		} 
	}
}
