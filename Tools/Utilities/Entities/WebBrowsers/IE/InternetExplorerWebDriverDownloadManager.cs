using System.Diagnostics;

using HtmlAgilityPack;
using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.WebBrowsers.IE
{
	public sealed class InternetExplorerWebDriverDownloadManager
		: ISoftwareDownloadManager
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly IDownloadAndCleanUpManager downloadAndCleanUp;
		private readonly ISoftwareDownloadLogger downloadLogger;

		private const string SELENIUM_DOWNLOADS_BASE_URL = "https://www.selenium.dev/downloads/";

		public Software DownloadsSoftware => Software.InternetExplorerWebDriver;

		public InternetExplorerWebDriverDownloadManager (
			IHttpClientFactory httpClientFactory,
			IDownloadAndCleanUpManager downloadAndCleanUp,
			ISoftwareDownloadLogger downloadLogger)
		{
			this.httpClientFactory = httpClientFactory;
			this.downloadAndCleanUp = downloadAndCleanUp;
			this.downloadLogger = downloadLogger;
		}

		public async Task<bool> DownloadLatestSoftwareAsync (string downloadPath)
		{
			try
			{
				var httpClient = this.httpClientFactory.CreateClient ();

				// Get Selenium downloads page HTML document.
				var pageContent = await httpClient.GetStringAsync (SELENIUM_DOWNLOADS_BASE_URL);

				// Prepare to scrape the HTML document.
				var htmlPage = new HtmlDocument ();
				htmlPage.LoadHtml (pageContent);

				// Locate the download Anchor Tag.
				var downloadLink
					= htmlPage
						.DocumentNode
						.SelectSingleNode ("//a[contains(text(), '32 bit Windows IE')]")
						?.GetAttributeValue ("href", string.Empty);

				if (string.IsNullOrEmpty (downloadLink))
				{
					return false;
				}

				var ieWebDriverFromSeleniumUrl = downloadLink.StartsWith ("http") ? downloadLink : "https://www.selenium.dev" + downloadLink;

				await this.downloadAndCleanUp.DownloadSoftwareAndCleanUp (downloadPath, ieWebDriverFromSeleniumUrl);

				// Prepare to read the version of the IE Web Driver from Selenium's download page.
				var version = string.Empty;
				var ieDriverPath = Path.Combine (downloadPath, "IEDriverServer.exe");

				if (File.Exists (ieDriverPath))
				{
					// Read file version.
					var versionInfo = FileVersionInfo.GetVersionInfo (ieDriverPath);
					version = versionInfo.FileVersion;
				}

				await this.downloadLogger
					.LogWebDriverInfo (
						DownloadsSoftware,
						downloadPath,
						version,
						ieWebDriverFromSeleniumUrl);

				return true;

			}
			catch (Exception ex)
			{
				Console.WriteLine ($"Error while downloading latest web driver software.\r\n\r\nMessage: {ex.Message}\r\n\r\nStackTrace: {ex.StackTrace}");
				return false;
			}
		}
	}
}