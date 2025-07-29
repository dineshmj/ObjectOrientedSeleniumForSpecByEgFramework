using SimpleQA.Framework.Abstractions;
using SimpleQA.Framework.Selenium.Abstractions;
using SimpleQA.UseCases.Selenium.WebUITests.LetsKodeTests.Pages;

namespace SimpleQA.UseCases.Selenium.WebUITests.LetsKodeTests.Navigation
{
	public sealed class LetsKodeNavigationComponent<UserRole, TestEnvironment>
		: SeleniumNavigationComponentBase<UserRole, TestEnvironment>
		where UserRole : Enum
		where TestEnvironment : Enum
	{
		// Constructor.
		public LetsKodeNavigationComponent (
				IEnvironmentDataProvider<UserRole, TestEnvironment> testDataProvider,
				IDecryptor decryptor = null)
			: base (testDataProvider, decryptor)
		{
		}

		public LetsKodeItPage GoToLetsKodePage ()
		{
			return
				new LetsKodeItPage (
					WebDriver,
					EnvironmentDataProvider.GetApplicationUrlFor (base.ExecutionEnvironment),
					navigationRequired: true,
					maximizeWindow: true);
		}

		public LetsKodeItPracticePage GoToLetsKodePracticePage ()
		{
			return
				new LetsKodeItPracticePage (
					WebDriver,
					EnvironmentDataProvider.GetApplicationUrlFor (base.ExecutionEnvironment),
					navigationRequired: true,
					maximizeWindow: true);
		}
	}
}