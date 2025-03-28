using OOSelenium.Framework.Abstractions;
using SampleWebApp.UiTests.LetsKodeTests.Pages;

namespace SampleWebApp.UiTests.LetsKodeTests.Navigation
{
	public sealed class LetsKodeNavigationComponent<UserRole, TestEnvironment>
		: WebUiNavigationComponentBase<UserRole, TestEnvironment>
	{
		// Constructor.
		public LetsKodeNavigationComponent (
				IExecutionEnvironmentPageDataProvider<UserRole, TestEnvironment> testDataProvider,
				IDecryptor decryptor = null)
			: base (testDataProvider, decryptor)
		{
		}

		public LetsKodeItPage GoToLetsKodePage ()
		{
			return
				new LetsKodeItPage (
					WebDriver,
					ExecutionEnvironmentPageDataProvider.GetWebApplicationUrlFor (base.ExecutionEnvironment));
		}
	}
}