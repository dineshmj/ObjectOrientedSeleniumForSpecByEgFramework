using Microsoft.Extensions.Configuration;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Misc;

namespace OOSelenium.Framework.Abstractions
{
	// Acts as the abstract base class for all business navgiation components.
	public abstract class WebUiNavigationComponentBase<TUserRole, TExecutionEnvironment>
		: IWebUiNavigationComponent<TUserRole, TExecutionEnvironment>
	{
		private readonly IConfigurationRoot appSettings;

		// Properties.
		public IExecutionEnvironmentPageDataProvider<TUserRole, TExecutionEnvironment> ExecutionEnvironmentPageDataProvider { get; protected set; }

		public IDecryptor? Decryptor { get; protected set; }

		public TExecutionEnvironment ExecutionEnvironment { get; protected set; }

		public WebBrowser PreferredWebBrowser { get; protected set; }

		public IWebDriver WebDriver { get; protected set; }

		// Constructors.
		protected WebUiNavigationComponentBase (
				IWebUiNavigationComponent<TUserRole, TExecutionEnvironment> parentNavigationComponent)
			: this (parentNavigationComponent.ExecutionEnvironmentPageDataProvider, parentNavigationComponent.Decryptor)
		{
		}

		protected WebUiNavigationComponentBase
			(
				IExecutionEnvironmentPageDataProvider<TUserRole, TExecutionEnvironment> exeEnvDataProvider,
				IDecryptor? decryptor = null
			)
		{
			// Config reader.
			this.appSettings = new ConfigurationBuilder ().AddJsonFile ("appsettings.json").Build ();

			// Ensure generic types are enums.
			this.EnsureGenericArgumentsAreEnumTypes ();

			this.ExecutionEnvironmentPageDataProvider = exeEnvDataProvider;
			this.Decryptor = decryptor;

			if (exeEnvDataProvider != null)
			{
				this.ExecutionEnvironment = exeEnvDataProvider.GetExecutionEnvironment ();
				this.PreferredWebBrowser = exeEnvDataProvider.GetPreferredWebBrowser ();

				// Selenium Grid related settings.
				var runMode
					= (this.appSettings [ConfigKeys.RUN_MODE].Trim ().Equals ("local"))
						? TestRunMode.Local
						: TestRunMode.SeleniumGrid;

				var gridHubUrl = String.Empty;

				if (runMode == TestRunMode.SeleniumGrid)
				{
					gridHubUrl = this.appSettings [ConfigKeys.SELENIUM_GRID_HUB_URL];
				}

				string? browserExeAbsolutePath;
				string? webDriverExeDirectoryAbsolutePath;

				// Browser and web driver executable paths.
				switch (this.PreferredWebBrowser)
				{
					case WebBrowser.MicrosoftEdge:
						browserExeAbsolutePath = appSettings [ConfigKeys.EDGE_BROWSER_EXE_ABSOLUTE_PATH];
						webDriverExeDirectoryAbsolutePath = appSettings [ConfigKeys.EDGE_WEB_DRIVER_EXE_DIRECTORY_PATH];
						break;

					case WebBrowser.GoogleChrome:
						browserExeAbsolutePath = appSettings [ConfigKeys.CHROME_BROWSER_EXE_ABSOLUTE_PATH];
						webDriverExeDirectoryAbsolutePath = appSettings [ConfigKeys.CHROME_WEB_DRIVER_EXE_DIRECTORY_PATH];
						break;

					case WebBrowser.MozillaFirefox:
						browserExeAbsolutePath = appSettings [ConfigKeys.FIREFOX_BROWSER_EXE_ABSOLUTE_PATH];
						webDriverExeDirectoryAbsolutePath = appSettings [ConfigKeys.FIREFOX_WEB_DRIVER_EXE_DIRECTORY_PATH];
						break;

					case WebBrowser.InternetExplorer:
						browserExeAbsolutePath = appSettings [ConfigKeys.IE_BROWSER_EXE_ABSOLUTE_PATH];
						webDriverExeDirectoryAbsolutePath = appSettings [ConfigKeys.IE_WEB_DRIVER_EXE_DIRECTORY_PATH];
						break;

					default:
						throw new NotImplementedException ($"The Object Oriented Selenium Framework does not support web browser '{this.PreferredWebBrowser}' yet.");
				}

				// Prepare the web driver of choice.
				switch (this.PreferredWebBrowser)
				{
					case WebBrowser.MozillaFirefox:
						if (runMode == TestRunMode.Local)
						{
							var firefoxOptions = new FirefoxOptions { BinaryLocation = browserExeAbsolutePath };

							firefoxOptions.AcceptInsecureCertificates = true;

							this.WebDriver = new FirefoxDriver (webDriverExeDirectoryAbsolutePath, firefoxOptions);
						}
						else
						{
							// NOTE: The browser EXE path should be that of the Grid server, not local.
							var firefoxOptions = new FirefoxOptions () { BinaryLocation = browserExeAbsolutePath };

							firefoxOptions.AcceptInsecureCertificates = true;
							this.WebDriver = new RemoteWebDriver (new Uri (gridHubUrl), firefoxOptions);
						}
						break;

					case WebBrowser.GoogleChrome:

						if (runMode == TestRunMode.Local)
						{
							var chromeOptions = new ChromeOptions { BinaryLocation = browserExeAbsolutePath };

							chromeOptions.AddArguments ("--ignore-certificate-errors");
							chromeOptions.AddAdditionalChromeOption ("useAutomationExtension", false);
							chromeOptions.AddArgument ("no-sandbox");

							this.WebDriver = new ChromeDriver (webDriverExeDirectoryAbsolutePath, chromeOptions);
						}
						else
						{
							// NOTE: If the binary cannot be obtained while running in grid mode, then you might need to specify the HUB server Chrome binary path.
							var chromeOptions = new ChromeOptions ();

							chromeOptions.AddArguments ("--ignore-certificate-errors");
							chromeOptions.AddAdditionalChromeOption ("useAutomationExtension", false);
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

							edgeOptions.AddArguments ("--ignore-certificate-errors");

							var edgeService = EdgeDriverService.CreateDefaultService (webDriverExeDirectoryAbsolutePath, "msedgedriver.exe");
							this.WebDriver = new EdgeDriver (edgeService, edgeOptions);
						}
						else
						{
							var edgeOptions = new EdgeOptions ();
							edgeOptions.AddArguments ("--ignore-certificate-errors");

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
									IgnoreZoomLevel = true,
									RequireWindowFocus = true,
									IntroduceInstabilityByIgnoringProtectedModeSettings = true
								};

							ieOptions.AcceptInsecureCertificates = true;
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

							ieOptions.AcceptInsecureCertificates = true;
							ieOptions.AddAdditionalInternetExplorerOption ("useAutomationExtension", false);
							this.WebDriver = new RemoteWebDriver (new Uri (gridHubUrl), ieOptions);
						}
						break;

					default:
						throw new NotImplementedException ($"The specified web browser \"{ this.PreferredWebBrowser }\" is not yet implemented for acceptance tests.");
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
				{
					throw new InvalidOperationException ("URL is not set yet; cannot navigate.");
				}
			}

			// Navigate to the URL.
			this.WebDriver.Navigate ();
		}

		// Private methods.
		private void EnsureGenericArgumentsAreEnumTypes ()
		{
			if (typeof (TUserRole).IsEnum && typeof (TExecutionEnvironment).IsEnum)
			{
				return;
			}

			throw new InvalidOperationException ("The specified generic types must be Enums.");
		}

		public virtual void Dispose ()
		{
			this.WebDriver?.Quit ();
			this.WebDriver?.Dispose ();
		}
	}
}