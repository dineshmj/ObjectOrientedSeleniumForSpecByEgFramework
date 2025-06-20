using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.WebUIControls;
using OOSelenium.Framework.Entities;

namespace OOSelenium.WebUiTests.LetsKodeTests.Pages
{
	public sealed class LetsKodeItPage
		: WebUiPageBase
	{
		public RadioButtons CarsRadioButtons { get; private set; }
		
		public DropDownList CarsDropDownList { get; private set; }

		public MultiSelectListBox FruitsMultiSelectListBox { get; private set; }

		public CheckBox BmwCheckBox { get; private set; }

		public CheckBox BenzCheckBox { get; private set; }

		public CheckBox HondaCheckBox { get; private set; }

		public LetsKodeItPage (IWebDriver webDriver, string baseUrl, bool navigationRequired, bool maximizeWindow)
			: base (webDriver, baseUrl, navigationRequired, maximizeWindow)
		{
			this.CarsRadioButtons = base.FindRadioButtonGroupByName ("cars");
			this.CarsDropDownList = base.FindDropDownListByName ("cars");
			this.FruitsMultiSelectListBox = base.FindMultiSelectListBoxByName ("multiple-select-example");

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