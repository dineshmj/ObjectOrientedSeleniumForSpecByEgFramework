using OOSelenium.Framework.Abstractions;

using SampleWebApp.UiTests.UiInteractionSample.Helpers.Screens;

namespace SampleWebApp.UiTests.UiInteractionSample.Helpers.Components
{
	public sealed class LetsKodeUiComponent<UserRole, TestEnvironment>
		: BusinessFunctionFlowComponentBase<UserRole, TestEnvironment>
	{
		// Constructor.
		public LetsKodeUiComponent
			(
				ITestBackgroundDataProvider<UserRole, TestEnvironment> testBackgroundDataProvider,
				IDecryptor decryptor = null
			)
			: base (testBackgroundDataProvider, decryptor)
		{
		}

		public LetsKodeItPage GetLetsKodePage ()
		{
			return
				new LetsKodeItPage (
					base.WebDriver,
					base.TestBackgroundDataProvider.GetTargetApplicationBaseUrlFor (base.TestEnvironment));
		}
	}
}