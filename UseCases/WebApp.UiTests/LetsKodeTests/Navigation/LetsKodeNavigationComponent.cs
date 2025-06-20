using OOSelenium.Framework.Abstractions;
using OOSelenium.WebUiTests.LetsKodeTests.Pages;

namespace OOSelenium.WebUiTests.LetsKodeTests.Navigation
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
					ExecutionEnvironmentPageDataProvider.GetWebApplicationUrlFor (base.ExecutionEnvironment),
					navigationRequired: true,
					maximizeWindow: true);
		}

		public LetsKodeItPracticePage GoToLetsKodePracticePage ()
		{
			return
				new LetsKodeItPracticePage (
					WebDriver,
					ExecutionEnvironmentPageDataProvider.GetWebApplicationUrlFor (base.ExecutionEnvironment),
					navigationRequired: true,
					maximizeWindow: true);
		}
	}
}