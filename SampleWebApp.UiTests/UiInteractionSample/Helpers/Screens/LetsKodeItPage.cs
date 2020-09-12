using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using OpenQA.Selenium;

namespace SampleWebApp.UiTests.UiInteractionSample.Helpers.Screens
{
	public sealed class LetsKodeItPage
		: WebUiPageBase
	{
		public RadioButtons CarRadioButtonGroup { get; private set; }

		public LetsKodeItPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			this.CarRadioButtonGroup = base.FindRadioButtonGroup ("cars");
		}
	}
}
