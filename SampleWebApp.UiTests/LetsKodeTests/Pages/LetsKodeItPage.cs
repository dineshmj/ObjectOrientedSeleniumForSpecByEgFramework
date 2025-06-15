using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;
using OOSelenium.Framework.Entities;

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
			CarRadioButtonGroup = FindRadioButtonGroupByName ("cars");
			CarDropDown = FindDropDownList ("cars");
			FruitsMultiListBox = FindMultiSelectListBox ("multiple-select-example");

			// Check boxes.
			BmwCheckBox
				= this.FindById<CheckBox> (
					"bmwcheck",
					(identifier, webElement, webDriver) => new CheckBox (webElement, identifier, LocateByWhat.Id, webDriver));

			BenzCheckBox
				= this.FindById<CheckBox> (
					"benzcheck",
					(identifier, webElement, webDriver) => new CheckBox (webElement, identifier, LocateByWhat.Id, webDriver));

			HondaCheckBox
				= this.FindById<CheckBox> (
					"hondacheck",
					(identifier, webElement, webDriver) => new CheckBox (webElement, identifier, LocateByWhat.Id, webDriver));
		}
	}
}