using System;
using System.Configuration;

using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Misc;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace OOSelenium.Framework.Abstractions
{
	/// <summary>
	/// Acts as the abstract base class for all business function flow components.
	/// </summary>
	public abstract class BusinessFunctionFlowComponentBase<TUserRole, TTestEnvironment>
		: IBusinessFunctionFlowComponent<TUserRole, TTestEnvironment>
	{
		// Properties.
		public ITestBackgroundDataProvider<TUserRole, TTestEnvironment> TestBackgroundDataProvider { get; protected set; }

		public TTestEnvironment TestEnvironment { get; protected set; }

		public WebBrowser WebBrowserToUse { get; protected set; }

		public IWebDriver WebDriver { get; protected set; }

		// Constructors.
		protected BusinessFunctionFlowComponentBase (IBusinessFunctionFlowComponent<TUserRole, TTestEnvironment> hostComponent)
			: this (hostComponent.TestBackgroundDataProvider)
		{
		}

		protected BusinessFunctionFlowComponentBase (ITestBackgroundDataProvider<TUserRole, TTestEnvironment> testBackgroundDataProvider)
		{
			// Ensure generic types are enums.
			this.EnsureGenericArgumentsAreEnumTypes ();

			this.TestBackgroundDataProvider = testBackgroundDataProvider;

			if (testBackgroundDataProvider != null)
			{
				this.TestEnvironment = testBackgroundDataProvider.GetTargetTestEnvironment ();
				this.WebBrowserToUse = testBackgroundDataProvider.GetWebBrowserTypeToUseForAcceptanceTests ();

				var appSettings = ConfigurationManager.AppSettings;
				var configKeyFirstPart = this.WebBrowserToUse.ToString ();

				var browserExeAbsolutePath = appSettings [configKeyFirstPart + Constants.BROWSER_EXE_ABSOLUTE_PATH_KEY_PART];
				var webDriverExeDirectoryAbsolutePath = appSettings [configKeyFirstPart + Constants.WEB_DRIVER_EXE_DIRECTORY_PATH_KEY_PART];

				// Prepare the web driver of choice.
				switch (this.WebBrowserToUse)
				{
					case WebBrowser.MozillaFirefox:
						var firefoxOptions = new FirefoxOptions { BrowserExecutableLocation = browserExeAbsolutePath };
						this.WebDriver = new FirefoxDriver (webDriverExeDirectoryAbsolutePath, firefoxOptions);
						break;

					case WebBrowser.GoogleChrome:
						var chromeOptions = new ChromeOptions { BinaryLocation = browserExeAbsolutePath };
						chromeOptions.AddAdditionalCapability ("useAutomationExtension", false);
						this.WebDriver = new ChromeDriver (webDriverExeDirectoryAbsolutePath, chromeOptions);
						break;

					case WebBrowser.InternetExplorer:
						var ieOptions = new InternetExplorerOptions { EnsureCleanSession = true, RequireWindowFocus = true };
						this.WebDriver = new InternetExplorerDriver (webDriverExeDirectoryAbsolutePath, ieOptions);
						break;

					default:
						throw new NotImplementedException ($"The specified web browser \"{ this.WebBrowserToUse }\" is not yet implemented for acceptance tests.");
				}
			}
		}

		// Public methods.
		public virtual void Navigate (string url = "")
		{
			if (String.IsNullOrEmpty (url) == false)
			{
				this.WebDriver.Url = url;
			}
			else
			{
				if (String.IsNullOrEmpty (this.WebDriver.Url))
					throw new InvalidOperationException ("URL is not set yet; cannot navigate.");
			}

			// Navigate to the URL.
			this.WebDriver.Navigate ();
		}

		// Private methods.
		private void EnsureGenericArgumentsAreEnumTypes ()
		{
			if (typeof (TUserRole).IsEnum && typeof (TTestEnvironment).IsEnum)
				return;

			throw new InvalidOperationException ("The specified generic types must be Enums.");
		}

		public virtual void Dispose ()
		{
			this.WebDriver?.Quit ();
			this.WebDriver?.Dispose ();
		}
	}
}
