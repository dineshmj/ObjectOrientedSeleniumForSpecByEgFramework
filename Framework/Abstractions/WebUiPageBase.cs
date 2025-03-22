using OOSelenium.Framework.WebUIControls;

using OpenQA.Selenium;

namespace OOSelenium.Framework.Abstractions
{
	public abstract class WebUiPageBase
		: IDisposable
	{
		// Protected fields.
		protected readonly IWebDriver webDriver;
		protected readonly string baseUrl;

		// Properties.
		public IWebDriver WebDriver { get { return this.webDriver; } }
		
		public string Title { get { return this.webDriver?.Title; } }

		// Constructor.
		public WebUiPageBase (IWebDriver webDriver, string baseUrl)
		{
			this.webDriver = webDriver;
			this.baseUrl = baseUrl;

			// Go to the page, so that its UI fields can be instantiated.
			this.NavigateToPage ();
		}

		// Public methods.
		public virtual void NavigateToPage ()
		{
			this.webDriver.Manage ().Window.Maximize ();
			this.webDriver.Navigate ().GoToUrl (this.baseUrl);
		}

		// Protected methods.
		protected Link FindLink (string linkId)
		{
			return new Link (this.webDriver.FindElement (By.Id (linkId)), linkId, this.webDriver);
		}

		protected Picture FindImage (string imageId)
		{
			return new Picture (this.webDriver.FindElement (By.Id (imageId)), imageId, this.webDriver);
		}


		protected Label FindLabel (string labelId)
		{
			return new Label (this.webDriver.FindElement (By.Id (labelId)), labelId, this.webDriver);
		}

		protected ValidationSummary FindValidationSummary (string validationSummaryId)
		{
			return new ValidationSummary (this.webDriver.FindElement (By.Id (validationSummaryId)), validationSummaryId, this.webDriver);
		}

		protected ValidationLabel FindValidationLabel (string validationLabelId)
		{
			return new ValidationLabel (this.webDriver.FindElement (By.Id (validationLabelId)), validationLabelId, this.webDriver);
		}

		protected TextField FindTextField (string textFieldId)
		{
			return new TextField (this.webDriver.FindElement (By.Id (textFieldId)), textFieldId, this.webDriver);
		}

		protected CheckBox FindCheckBox (string checkBoxId)
		{
			return new CheckBox (this.webDriver.FindElement (By.Id (checkBoxId)), checkBoxId, this.webDriver);
		}

		protected Button FindButton (string buttonId)
		{
			return new Button (this.webDriver.FindElement (By.Id (buttonId)), buttonId, this.webDriver);
		}

		protected RadioButtons FindRadioButtonGroup (string radioButtonGroupName)
		{
			return new RadioButtons (this.WebDriver.FindElements (By.XPath ($"//input[@name=\"{ radioButtonGroupName }\" and @type=\"radio\"]")), radioButtonGroupName, this.webDriver);
		}

		protected DropDownList FindDropDownList (string dropDownName)
		{
			var selectElement = this.WebDriver.FindElement (By.XPath ($"//select[@name=\"{ dropDownName }\"]"));

			if (selectElement == null)
			{
				// Perhaps, the test engineer would have passed "id" instead of the name attribute.
				// Try getting the select tag based on "id".
				selectElement = this.WebDriver.FindElement (By.Id (dropDownName));
			}

			var selectOptionElements = selectElement?.FindElements (By.XPath ("./option"));

			return new DropDownList (selectOptionElements, dropDownName, this.webDriver);
		}

		protected MultiSelectListBox FindMultiSelectListBox (string multiListName)
		{
			// "multiple" attribute must be present for a multi-select list box.
			var selectElement = this.WebDriver.FindElement (By.XPath ($"//select[@name=\"{ multiListName }\" and @multiple]"));

			if (selectElement == null)
			{
				// Perhaps, the test engineer would have passed "id" instead of the name attribute.
				// Try getting the select tag based on "id".
				selectElement = this.WebDriver.FindElement (By.Id (multiListName));
			}

			var selectOptionElements = selectElement?.FindElements (By.XPath ("./option"));

			return new MultiSelectListBox (selectOptionElements, multiListName, this.webDriver);
		}

		public virtual void Dispose ()
		{
			this.Dispose (true);
		}

		protected virtual void Dispose (bool proceedWithDisposal)
		{
			if (proceedWithDisposal)
			{
				this.webDriver?.Quit ();
				this.webDriver?.Dispose ();
			}
		}
	}
}