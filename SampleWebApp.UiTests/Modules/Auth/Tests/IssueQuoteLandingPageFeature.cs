using System;

using FluentAssertions;
using Xbehave;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using SampleWebApp.UiTests.Entities;
using SampleWebApp.UiTests.Modules.Auth.Helpers;
using SampleWebApp.UiTests.Modules.Auth.Helpers.Components;
using SampleWebApp.UiTests.Modules.Auth.Helpers.Screens;
using SampleWebApp.UiTests.Preparatory;

namespace SampleWebApp.UiTests.Modules.Auth.Tests
{
	public sealed class IssueQuoteLandingPageFeature
		: WebUiTestBase
	{
		private readonly ITestBackgroundDataProvider<UserRole, TestEnvironment> dataProvider;
		private readonly IDecryptor decryptor;
		private readonly SignInFlowComponent<UserRole, TestEnvironment> signInComponent;
		private QuoteIssuerLandingPage issueQuotesLandingPage;

		public IssueQuoteLandingPageFeature ()
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
		public async void Issue_Quotes_landing_page_must_have_its_title_and_links_with_correct_texts ()
		{
			var pageTitle = String.Empty; var homeLink = (Link) null; var signOutLink = (Link) null;
			var searchQuoteLink = (Link) null; var issueNewQuoteLink = (Link) null;

			"Given that a \"Quote Issuer\" user has successfully signed in to the Insurance One application"
				.x (() =>
				{
					this.issueQuotesLandingPage
						= this.signInComponent
							.GetLandingPageFor<QuoteIssuerLandingPage> (UserRole.QuoteIssuer);
				});

			"When I get the Issue Quotes landing page after successful signing in"
				.x (() =>
				{
					// Read the UI field labels.
					pageTitle = this.issueQuotesLandingPage.Title;

					// Links on the page.
					homeLink = this.issueQuotesLandingPage.HomeLink;
					signOutLink = this.issueQuotesLandingPage.SignOutLink;
					searchQuoteLink = this.issueQuotesLandingPage.SearchQuotesLink;
					issueNewQuoteLink = this.issueQuotesLandingPage.IssueANewQuoteLink;
				});

			"Then the page title, and the links on the Issue Quotes landing page must be as expected."
				.x (() =>
				{
					// Assert expectations.
					pageTitle.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_TITLE);

					homeLink.Text.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_HOME_LINK_TEXT);
					signOutLink.Text.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_SIGN_OUT_LINK_TEXT);
					searchQuoteLink.Text.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_SEARCH_QUOTES_LINK_TEXT);
					issueNewQuoteLink.Text.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_ISSUE_A_NEW_QUOTE_LINK_TEXT);
				});
		}

		public override void Dispose ()
		{
			this.signInComponent.Dispose ();
			this.issueQuotesLandingPage.Dispose ();
		}
	}
}
