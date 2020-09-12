using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

using OpenQA.Selenium;

namespace SampleWebApp.UiTests.UiInteractionSample.Helpers.Screens
{
	public sealed class LetsKodeItPage
		: WebUiPageBase
	{
		public RadioButtons CarRadioButtonGroup { get; private set; }
		
		public DropDownList CarDropDown { get; private set; }

		public MultiSelectListBox FruitsMultiListBox { get; private set; }

		public LetsKodeItPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			this.CarRadioButtonGroup = base.FindRadioButtonGroup ("cars");
			this.CarDropDown = base.FindDropDownList ("cars");
			this.FruitsMultiListBox = base.FindMultiSelectListBox ("multiple-select-example");
		}
	}
}
