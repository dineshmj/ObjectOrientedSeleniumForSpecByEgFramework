using OOSelenium.Framework.Abstractions;
using SampleWebApp.UiTests.LetsKodeTests.Pages;

namespace SampleWebApp.UiTests.LetsKodeTests.Navigation
{
	public sealed class LetsKodeNavigationComponent<UserRole, TestEnvironment>
		: BusinessNavigationComponentBase<UserRole, TestEnvironment>
	{
		// Constructor.
		public LetsKodeNavigationComponent (
				IExecutionEnvironmentDataProvider<UserRole, TestEnvironment> testDataProvider,
				IDecryptor decryptor = null)
			: base (testDataProvider, decryptor)
		{
		}

		public LetsKodeItPage GoToLetsKodePage ()
		{
			return
				new LetsKodeItPage (
					WebDriver,
					ExecutionEnvironmentDataProvider.GetApplicationBaseUrlFor (base.ExecutionEnvironment));
		}
	}
}