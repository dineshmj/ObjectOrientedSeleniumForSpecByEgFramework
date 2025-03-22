using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;

namespace SampleWebApp.UiTests.LetsKodeTests.Pages
{
	public sealed class LetsKodeItPage
		: WebUiPageBase
	{
		public RadioButtons CarRadioButtonGroup { get; private set; }
		
		public DropDownList CarDropDown { get; private set; }

		public MultiSelectListBox FruitsMultiListBox { get; private set; }

		public CheckBox BmwCheckBox { get; private set; }

		public CheckBox BenzCheckBox { get; private set; }

		public CheckBox HondaCheckBox { get; private set; }

		public LetsKodeItPage (IWebDriver webDriver, string baseUrl)
			: base (webDriver, baseUrl)
		{
			CarRadioButtonGroup = FindRadioButtonGroup ("cars");
			CarDropDown = FindDropDownList ("cars");
			FruitsMultiListBox = FindMultiSelectListBox ("multiple-select-example");

			// Check boxes.
			BmwCheckBox = FindCheckBox ("bmwcheck");
			BenzCheckBox = FindCheckBox ("benzcheck");
			HondaCheckBox = FindCheckBox ("hondacheck");
		}
	}
}