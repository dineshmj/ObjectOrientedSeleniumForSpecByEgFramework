using System;

using OOSelenium.Framework.WebUIControls;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

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
			this.GoToPage ();
		}

		// Public methods.
		public virtual void GoToPage ()
		{
			this.webDriver.Manage ().Window.Maximize ();
			this.webDriver.Navigate ().GoToUrl (this.baseUrl);
		}

		public virtual void Dispose ()
		{
			this.Dispose (true);
		}

		// Protected methods.
		protected Link FindLink (string linkId)
		{
			return new Link (this.webDriver.FindElement (By.Id (linkId)) as RemoteWebElement, linkId);
		}

		protected Label FindLabel (string labelId)
		{
			return new Label (this.webDriver.FindElement (By.Id (labelId)) as RemoteWebElement, labelId);
		}

		protected ValidationSummary FindValidationSummary (string validationSummaryId)
		{
			return new ValidationSummary (this.webDriver.FindElement (By.Id (validationSummaryId)) as RemoteWebElement, validationSummaryId);
		}

		protected ValidationLabel FindValidationLabel (string validationLabelId)
		{
			return new ValidationLabel (this.webDriver.FindElement (By.Id (validationLabelId)) as RemoteWebElement, validationLabelId);
		}

		protected TextField FindTextField (string textFieldId)
		{
			return new TextField (this.webDriver.FindElement (By.Id (textFieldId)) as RemoteWebElement, textFieldId);
		}

		protected Button FindButton (string buttonId)
		{
			return new Button (this.webDriver.FindElement (By.Id (buttonId)) as RemoteWebElement, buttonId);
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