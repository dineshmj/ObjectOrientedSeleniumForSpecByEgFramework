namespace OOSelenium.Framework.Misc
{
	public static class ConfigKeys
	{
		// Run mode and preferred browser.
		public const string RUN_MODE = "webUITestConfig:seleniumTestRunMode";
		public const string PREFERRED_WEB_BROWSER = "webUITestConfig:preferredWebBrowser";

		// Selenium Grid related settings.
		public const string SELENIUM_GRID_HUB_URL = "webUITestConfig:seleniumGridConfig:gridHubUrl";
		public const string SELENIUM_GRID_PLATFORM = "webUITestConfig:seleniumGridConfig:gridPlatform";

		// Web drivers.
		public const string EDGE_BROWSER_EXE_ABSOLUTE_PATH = "webUITestConfig:webDriverConfig:MicrosoftEdge:exeAbsolutePath";
		public const string EDGE_WEB_DRIVER_EXE_DIRECTORY_PATH = "webUITestConfig:webDriverConfig:MicrosoftEdge:webDriverExeDirectoryAbsolutePath";

		public const string CHROME_BROWSER_EXE_ABSOLUTE_PATH = "webUITestConfig:webDriverConfig:GoogleChrome:exeAbsolutePath";
		public const string CHROME_WEB_DRIVER_EXE_DIRECTORY_PATH = "webUITestConfig:webDriverConfig:GoogleChrome:webDriverExeDirectoryAbsolutePath";

		public const string FIREFOX_BROWSER_EXE_ABSOLUTE_PATH = "webUITestConfig:webDriverConfig:MozillaFirefox:exeAbsolutePath";
		public const string FIREFOX_WEB_DRIVER_EXE_DIRECTORY_PATH = "webUITestConfig:webDriverConfig:MozillaFirefox:webDriverExeDirectoryAbsolutePath";

		public const string IE_BROWSER_EXE_ABSOLUTE_PATH = "webUITestConfig:webDriverConfig:InternetExplorer:exeAbsolutePath";
		public const string IE_WEB_DRIVER_EXE_DIRECTORY_PATH = "webUITestConfig:webDriverConfig:InternetExplorer:webDriverExeDirectoryAbsolutePath";
	}
}