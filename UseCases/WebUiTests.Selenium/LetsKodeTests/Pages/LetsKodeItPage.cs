using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;
using SimpleQA.Framework.Selenium.WebUIControls;

namespace SimpleQA.UseCases.Selenium.WebUITests.LetsKodeTests.Pages
{
	public sealed class LetsKodeItPage
		: SeleniumPageBase
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