using FluentAssertions;
using Xbehave;

using OOSelenium.Framework.Abstractions;
using OOSelenium.WebUiTests.OtherTests.Background;
using OOSelenium.WebUiTests.OtherTests.Navigation;
using OOSelenium.WebUiTests.OtherTests.Pages;
using OOSelenium.WebUiTests.TestWebAppTests.Background;

namespace OOSelenium.WebUiTests.TestWebAppTests.Tests
{
	public sealed class CbaComAuTests
		: WebUiTestBase
	{
		private readonly IExecutionEnvironmentPageDataProvider<UserRole, ExecutionEnvironment> dataProvider;
		private readonly IDecryptor decryptor;
		private readonly CbaComAuNavigationComponent<UserRole, ExecutionEnvironment> cbaNavigationComponent;
		private CommBankPage commBankPage;

		public CbaComAuTests ()
		{
			dataProvider = new CbaComAuDataProvider ();
			decryptor = new PassThroughDecryptor ();
			cbaNavigationComponent
				= new CbaComAuNavigationComponent<UserRole, ExecutionEnvironment>
					(
						dataProvider,
						decryptor
					);
		}

		[Scenario]
		public void MustBe_AbleTo_SearchFrom_LandingPage_WithoutSigningIn ()
		{
			IList<string> validationSummaryMessages = null;

			"Given that cba.com.au page is accessible"
				.x (() =>
				{
					// Go to the login page.
					commBankPage = cbaNavigationComponent.GoToCbaComAuPage ();
				});

			"When I click on \"Search\" icon, type \"Bank Loan\", and press RETURN"
				.x (() =>
				{
					commBankPage.SearchLensSpan.Click ();
					commBankPage.SearchTextField.SendKeys ("Bank Loan");
					commBankPage.SearchTextField.TypeEachCharacter ("\r\n");
				});

			"Then it should list the `Bank Loan` link among the search results shown on the page."
			.x (() =>
			{
				var bankLoanLink = commBankPage.BankLoanLink;
				bankLoanLink.Text.Should ().StartWith ("Bank loan to buy business");
			});
		}

		public override void Dispose ()
		{
			cbaNavigationComponent.Dispose ();
			commBankPage.Dispose ();
		}
	}
}