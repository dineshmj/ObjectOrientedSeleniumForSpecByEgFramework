using System;
using System.Configuration;
using System.Security.Policy;

using Microsoft.Edge.SeleniumTools;

using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Misc;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

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

		public IDecryptor Decryptor { get; protected set; }

		public TTestEnvironment TestEnvironment { get; protected set; }

		public WebBrowser WebBrowserToUse { get; protected set; }

		public IWebDriver WebDriver { get; protected set; }

		// Constructors.
		protected BusinessFunctionFlowComponentBase
			(
				IBusinessFunctionFlowComponent<TUserRole, TTestEnvironment> hostComponent
			)
			: this (hostComponent.TestBackgroundDataProvider, hostComponent.Decryptor)
		{
		}

		protected BusinessFunctionFlowComponentBase
			(
				ITestBackgroundDataProvider<TUserRole, TTestEnvironment> testBackgroundDataProvider,
				IDecryptor decryptor = null
			)
		{
			// Ensure generic types are enums.
			this.EnsureGenericArgumentsAreEnumTypes ();

			this.TestBackgroundDataProvider = testBackgroundDataProvider;
			this.Decryptor = decryptor;

			if (testBackgroundDataProvider != null)
			{
				this.TestEnvironment = testBackgroundDataProvider.GetTargetTestEnvironment ();
				this.WebBrowserToUse = testBackgroundDataProvider.GetWebBrowserTypeToUseForAcceptanceTests ();

				var appSettings = ConfigurationManager.AppSettings;

				// Selenium Grid related settings.
				var runMode
					= (appSettings [ConfigKeys.RUN_MODE].Trim ().Equals ("local"))
						? TestRunMode.Local
						: TestRunMode.SeleniumGrid;

				var gridHubUrl = String.Empty;

				if (runMode == TestRunMode.SeleniumGrid)
				{
					gridHubUrl = appSettings [ConfigKeys.SELENIUM_GRID_HUB_URL];
				}

				// Browser and web driver executable paths.
				var configKeyFirstPart = this.WebBrowserToUse.ToString ();

				var browserExeAbsolutePath = appSettings [configKeyFirstPart + ConfigKeys.BROWSER_EXE_ABSOLUTE_PATH_KEY_PART];
				var webDriverExeDirectoryAbsolutePath = appSettings [configKeyFirstPart + ConfigKeys.WEB_DRIVER_EXE_DIRECTORY_PATH_KEY_PART];

				// Prepare the web driver of choice.
				switch (this.WebBrowserToUse)
				{
					case WebBrowser.MozillaFirefox:
						if (runMode == TestRunMode.Local)
						{
							var firefoxOptions = new FirefoxOptions { BrowserExecutableLocation = browserExeAbsolutePath };
							this.WebDriver = new FirefoxDriver (webDriverExeDirectoryAbsolutePath, firefoxOptions);
						}
						else
						{
							var firefoxOptions = new FirefoxOptions ();
							this.WebDriver = new RemoteWebDriver (new Uri (gridHubUrl), firefoxOptions);
						}
						break;

					case WebBrowser.GoogleChrome:

						if (runMode == TestRunMode.Local)
						{
							var chromeOptions = new ChromeOptions { BinaryLocation = browserExeAbsolutePath };
							chromeOptions.AddAdditionalCapability ("useAutomationExtension", false);
							chromeOptions.AddArgument ("no-sandbox");
							this.WebDriver = new ChromeDriver (webDriverExeDirectoryAbsolutePath, chromeOptions);
						}
						else
						{
							var chromeOptions = new ChromeOptions ();
							chromeOptions.AddAdditionalCapability ("useAutomationExtension", false);
							chromeOptions.AddArgument ("no-sandbox");
							this.WebDriver = new RemoteWebDriver (new Uri (gridHubUrl), chromeOptions);
						}
						break;

					case WebBrowser.MicrosoftEdge:
						if (runMode == TestRunMode.Local)
						{
							var edgeOptions = new EdgeOptions
							{
								BinaryLocation = browserExeAbsolutePath
							};
							var edgeService = EdgeDriverService.CreateDefaultService (webDriverExeDirectoryAbsolutePath, "msedgedriver.exe");
							this.WebDriver = new EdgeDriver (edgeService, edgeOptions);
						}
						else
						{
							var edgeOptions = new EdgeOptions { UseChromium = true };
							this.WebDriver = new RemoteWebDriver (new Uri (gridHubUrl), edgeOptions);
						}
						break;

					case WebBrowser.InternetExplorer:
						if (runMode == TestRunMode.Local)
						{
							var ieOptions
								= new InternetExplorerOptions
								{
									EnsureCleanSession = true,
									RequireWindowFocus = true,
									IntroduceInstabilityByIgnoringProtectedModeSettings = true
								};

							ieOptions.AddAdditionalCapability ("useAutomationExtension", false);
							this.WebDriver = new InternetExplorerDriver (webDriverExeDirectoryAbsolutePath, ieOptions);
						}
						else
						{
							var ieOptions
								= new InternetExplorerOptions
								{
									EnsureCleanSession = true,
									RequireWindowFocus = true,
									IntroduceInstabilityByIgnoringProtectedModeSettings = true
								};

							ieOptions.AddAdditionalCapability ("useAutomationExtension", false);
							this.WebDriver = new RemoteWebDriver (new Uri (gridHubUrl), ieOptions);
						}
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
