using System;
using System.Collections.Generic;

using FluentAssertions;
using OOSelenium.Framework.Abstractions;
using Xbehave;

using SampleWebApp.UiTests.Entities;
using SampleWebApp.UiTests.Modules.Auth.Helpers;
using SampleWebApp.UiTests.Modules.Auth.Helpers.Components;
using SampleWebApp.UiTests.Modules.Auth.Helpers.Screens;
using SampleWebApp.UiTests.Preparatory;

namespace SampleWebApp.UiTests.Modules.Auth.Tests
{
	public sealed class AuthBasicFeature
		: WebUiTestBase
	{
		private readonly ITestBackgroundDataProvider<UserRole, TestEnvironment> dataProvider;
		private readonly SignInFlowComponent<UserRole, TestEnvironment> signInComponent;
		private SignInPage signInPage;

		public AuthBasicFeature ()
		{
			this.dataProvider = new InsuranceOneTestBackgroundDataProvider ();
			this.signInComponent = new SignInFlowComponent<UserRole, TestEnvironment> (dataProvider);
		}

		[Scenario]
		public async void Sign_in_page_must_have_its_title_and_UI_controls_with_correct_labels_and_texts ()
		{
			var userIdLabel = String.Empty; var passwordLabel = String.Empty; var signInButtonText = String.Empty; var pageTitle = String.Empty;

			"Given that Four Walls Inc. Insurance One sign in page is accessible"
				.x (() =>
				{
					signInPage = signInComponent.GoToSignInPage ();
				});

			"When I check the page title, labels of UI fileds, and text of buttons"
				.x (() =>
				{
					// Read the UI field labels.
					pageTitle = signInPage.Title;
					userIdLabel = signInPage.UserIdLabel.Text;
					passwordLabel = signInPage.PasswordLabel.Text;
					signInButtonText = signInPage.SignInButton.Text;
				});

			"Then the page title, labels and texts should be as expected"
				.x (() =>
				{
					// Assert expectations.
					pageTitle.Should ().Be (Expectations.SIGN_IN_PAGE_TITLE);
					userIdLabel.Should ().Be (Expectations.SIGN_IN_PAGE_USER_ID_FIELD_LABEL_TEXT);
					passwordLabel.Should ().Be (Expectations.SIGN_IN_PAGE_PASSWORD_FIELD_LABEL_TEXT);
					signInButtonText.Should ().Be (Expectations.SIGN_IN_PAGE_SIGN_IN_BUTTON_TEXT);
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
					signInPage = signInComponent.GoToSignInPage ();
				});

			"When enter invalid credentials on the sign in page and click the \"Sign in\" button"
				.x (() =>
				{
					// Enter invalid credentials and click "Sign in".
					signInPage.UserIdField.SetText ("pqr");
					signInPage.PasswordField.SetText ("xyz");
					signInPage.SignInButton.Click ();
				});

			"Then I should see relevant validation failure messages related to failed signing in attempt."
			.x (() =>
				{
					// Get the validation summary error messages.
					validationSummaryMessages = signInPage.ValidationSummary.ValidationFailureMessages;

					// Assert the expectations.
					validationSummaryMessages.Should ().NotBeNull ();
					validationSummaryMessages [0].Should ().Be (Expectations.FAILED_SIGNING_IN_ATTEMPT_MESSAGE);
				});
		} 
	}
}
