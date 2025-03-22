using FluentAssertions;
using Xbehave;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;
using SampleWebApp.UiTests.LoginTests.Background;
using SampleWebApp.UiTests.LoginTests.Navigation;
using SampleWebApp.UiTests.LoginTests.Pages;
using SampleWebApp.UiTests.LoginTests.DataProviders;

namespace SampleWebApp.UiTests.LoginTests.Tests
{
	public sealed class IssueQuotePageRenderTest
		: WebUiTestBase
	{
		private readonly IExecutionEnvironmentDataProvider<UserRole, ExecutionEnvironment> dataProvider;
		private readonly IDecryptor decryptor;
		private readonly LoginNavigationComponent<UserRole, ExecutionEnvironment> loginComponent;
		private IssueQuotesPage issueQuotesPage;

		public IssueQuotePageRenderTest ()
		{
			dataProvider = new InsuranceOneTestDataProvider ();
			decryptor = new PassThroughDecryptor ();
			loginComponent
				= new LoginNavigationComponent<UserRole, ExecutionEnvironment>
					(
						dataProvider,
						decryptor
					);
		}

		[Scenario]
		public async void TitleAndLinks_Of_IssueQuotesPage_MustBeCorrect ()
		{
			var pageTitle = string.Empty; var homeLink = (Link) null; var logoutLink = (Link) null;
			var searchQuoteLink = (Link) null; var issueNewQuoteLink = (Link) null;

			"Given that a \"Quote Issuer\" user has successfully logged in to the Insurance One application"
				.x (() =>
				{
					issueQuotesPage
						= loginComponent
							.LoginAndGoToPage<IssueQuotesPage> (UserRole.QuoteIssuer);
				});

			"When I get the Issue Quotes page after successful signing in"
				.x (() =>
				{
					// Read the UI field labels.
					pageTitle = issueQuotesPage.Title;

					// Links on the page.
					homeLink = issueQuotesPage.HomeLink;
					logoutLink = issueQuotesPage.LogoutLink;
					searchQuoteLink = issueQuotesPage.SearchQuotesLink;
					issueNewQuoteLink = issueQuotesPage.IssueANewQuoteLink;
				});

			"Then the page title, and the links on the Issue Quotes landing page must be as expected."
				.x (() =>
				{
					// Assert expectations.
					pageTitle.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_TITLE);

					homeLink.Text.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_HOME_LINK_TEXT);
					logoutLink.Text.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_LOGOUT_LINK_TEXT);
					searchQuoteLink.Text.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_SEARCH_QUOTES_LINK_TEXT);
					issueNewQuoteLink.Text.Should ().Be (Expectations.QUOTE_ISSUER_PAGE_ISSUE_A_NEW_QUOTE_LINK_TEXT);
				});
		}

		public override void Dispose ()
		{
			loginComponent.Dispose ();
			issueQuotesPage.Dispose ();
		}
	}
}