using System.Collections.ObjectModel;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

using OOSelenium.Framework.Extensions;
using OOSelenium.Framework.WebUIControls;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.Abstractions
{
	public abstract class WebUiPageBase
		: IDisposable
	{
		public sealed class CodeDomHelper
		{
			public readonly string FindRadioButtonGroupByName = nameof (WebUiPageBase.FindRadioButtonGroupByName);
			public readonly string FindDropDownList = nameof (WebUiPageBase.FindDropDownList);
			public readonly string FindMultiSelectListBox = nameof (WebUiPageBase.FindMultiSelectListBox);
		}

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

		// Protected methods - "Find" methods for various Web UI controls.

		protected TWebUiControl FindById<TWebUiControl> (string uniqueIdentifier, Func<string, IWebElement, IWebDriver, TWebUiControl> factory)
			where TWebUiControl : WebUiControlBase
		{
			var element = this.GetElementById (uniqueIdentifier);
			var webUiControl = factory (uniqueIdentifier, element, this.WebDriver);
			return webUiControl;
		}

		protected TWebUiControl FindByXPath<TWebUiControl> (string xPath, Func<string, IWebElement, IWebDriver, TWebUiControl> factory)
			where TWebUiControl : WebUiControlBase
		{
			var webElement = this.GetElementByXPath (xPath);
			var webUiControl = factory (xPath, webElement, this.WebDriver);
			return webUiControl;
		}

		protected TWebUiControl FindByName<TWebUiControl> (string cssClassName, Func<string, IWebElement, IWebDriver, TWebUiControl> factory)
			where TWebUiControl : WebUiControlBase
		{
			var webElement = this.GetElementByName (cssClassName);
			var webUiControl = factory (cssClassName, webElement, this.WebDriver);
			return webUiControl;
		}

		protected TWebUiControl FindByCssClass<TWebUiControl> (string cssClassName, Func<string, IWebElement, IWebDriver, TWebUiControl> factory)
			where TWebUiControl : WebUiControlBase
		{
			var webElement = this.GetElementByCss(cssClassName);
			var webUiControl = factory (cssClassName, webElement, this.WebDriver);
			return webUiControl;
		}

		protected RadioButtons FindRadioButtonGroupByName (string radioButtonGroupName)
		{
			var radioButtons = this.GetAllElementsByXPath ($"//input[@name=\"{radioButtonGroupName}\" and @type=\"radio\"]");
			return new RadioButtons (new ReadOnlyCollection<IWebElement> (radioButtons), radioButtonGroupName, LocateByWhat.Name, this.webDriver);
		}

		protected DropDownList FindDropDownList (string dropDownName)
		{
			var selectElement = this.GetElementByXPath ($"//select[@name=\"{ dropDownName }\"]");

			if (selectElement == null)
			{
				// Perhaps, the test engineer would have passed "id" instead of the name attribute.
				// Try getting the select tag based on "id".
				selectElement = this.GetElementById (dropDownName);
			}

			var selectOptionElements = selectElement?.FindElements (By.XPath ("./option"));

			return new DropDownList (selectOptionElements, dropDownName, LocateByWhat.Name, this.webDriver);
		}

		protected MultiSelectListBox FindMultiSelectListBox (string multiListName)
		{
			// "multiple" attribute must be present for a multi-select list box.
			var selectElement = this.GetElementByXPath ($"//select[@name=\"{ multiListName }\" and @multiple]");

			if (selectElement == null)
			{
				// Perhaps, the test engineer would have passed "id" instead of the name attribute.
				// Try getting the select tag based on "id".
				selectElement = this.GetElementById (multiListName);
			}

			var selectOptionElements = selectElement?.FindElements (By.XPath ("./option"));

			return new MultiSelectListBox (selectOptionElements, multiListName, LocateByWhat.Name, this.webDriver);
		}

		protected IList<Link> FindAllLinksByCss (string cssClassNameFromHtmlAsIs)
		{
			try
			{
				var linkElements = this.GetAllElementsByCss (cssClassNameFromHtmlAsIs.RefineForAnchor ());
				var links = new List<Link> ();

				foreach (var oneLinkElement in linkElements)
				{
					links.Add (new Link (oneLinkElement, cssClassNameFromHtmlAsIs, LocateByWhat.CssClass, this.webDriver));
				}

				return links;
			}
			catch (Exception ex)
			{
				return default;
			}
		}

		protected IList<Div> FindAllDivsByCss (string cssClassNameFromHtmlAsIs)
		{
			try
			{
				var divElements = this.GetAllElementsByCss (cssClassNameFromHtmlAsIs.RefineForDiv ());
				var divs = new List<Div> ();

				foreach (var oneDivElement in divElements)
				{
					divs.Add (new Div (oneDivElement, cssClassNameFromHtmlAsIs, LocateByWhat.CssClass, this.webDriver));
				}

				return divs;
			}
			catch (Exception ex)
			{
				return default;
			}
		}

		// Protected methods - "Get" methods for various Web UI elements.

		protected IWebElement GetElementById (string elementId)
		{
			var wait = new WebDriverWait (this.webDriver, TimeSpan.FromSeconds (20));
			var element = wait.Until (ExpectedConditions.ElementExists (By.Id (elementId)));
			return element;
		}

		protected IWebElement GetElementByName (string elementName)
		{
			var wait = new WebDriverWait (this.webDriver, TimeSpan.FromSeconds (20));
			var element = wait.Until (ExpectedConditions.ElementExists (By.Name (elementName)));
			return element;
		}

		protected IWebElement GetElementByCss (string refinedCssClassName)
		{
			var wait = new WebDriverWait (this.webDriver, TimeSpan.FromSeconds (20));
			var element = wait.Until (ExpectedConditions.ElementExists (By.CssSelector (refinedCssClassName)));
			return element;
		}

		protected IWebElement GetElementByXPath (string xPath)
		{
			var wait = new WebDriverWait (this.webDriver, TimeSpan.FromSeconds (20));
			var element = wait.Until (ExpectedConditions.ElementExists (By.XPath (xPath)));
			return element;
		}

		protected IList<IWebElement> GetAllElementsByCss (string refinedCssClassName)
		{
			var wait = new WebDriverWait (this.webDriver, TimeSpan.FromSeconds (20));
			var elements = wait.Until (ExpectedConditions.PresenceOfAllElementsLocatedBy (By.CssSelector (refinedCssClassName)));
			return elements;
		}

		protected IList<IWebElement> GetAllElementsByXPath (string xPath)
		{
			var wait = new WebDriverWait (this.webDriver, TimeSpan.FromSeconds (20));
			var elements = wait.Until (ExpectedConditions.PresenceOfAllElementsLocatedBy (By.XPath (xPath)));
			return elements;
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