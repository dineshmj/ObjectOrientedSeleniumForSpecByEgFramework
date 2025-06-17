namespace OOSelenium.Utilities.Entities.WebBrowsers.Chrome
{
	public sealed class ChromeWebDriverChannel
	{
		public string? Channel { get; set; }

		public string? Version { get; set; }

		public string? Revision { get; set; }

		public ChromeWebDriverDownloads? Downloads { get; set; }
	}
}