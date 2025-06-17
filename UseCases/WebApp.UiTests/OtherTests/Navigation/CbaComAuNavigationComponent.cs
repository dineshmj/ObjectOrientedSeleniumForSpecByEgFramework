using OOSelenium.Framework.Abstractions;
using OOSelenium.WebUiTests.OtherTests.Pages;

namespace OOSelenium.WebUiTests.OtherTests.Navigation
{
	public sealed class CbaComAuNavigationComponent<UserRole, TestEnvironment>
		: WebUiNavigationComponentBase<UserRole, TestEnvironment>
	{
		// Constructor.
		public CbaComAuNavigationComponent (
				IExecutionEnvironmentPageDataProvider<UserRole, TestEnvironment> testDataProvider,
				IDecryptor decryptor = null)
			: base (testDataProvider, decryptor)
		{
		}

		public CommBankPage GoToCbaComAuPage ()
		{
			return
				new CommBankPage (
					WebDriver,
					ExecutionEnvironmentPageDataProvider.GetWebApplicationUrlFor (base.ExecutionEnvironment));
		}
	}
}