using System.Diagnostics;

using HtmlAgilityPack;
using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.WebBrowsers.IE
{
	public sealed class InternetExplorerWebDriverDownloadManager
		: ISoftwareDownloadManager
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IDownloadAndCleanUpManager _downloadAndCleanUp;
		private readonly ISoftwareDownloadLogger _downloadLogger;

		private const string SELENIUM_DOWNLOADS_BASE_URL = "https://www.selenium.dev/downloads/";

		public Software DownloadsSoftware => Software.InternetExplorerWebDriver;

		public InternetExplorerWebDriverDownloadManager (
			IHttpClientFactory httpClientFactory,
			IDownloadAndCleanUpManager downloadAndCleanUp,
			ISoftwareDownloadLogger downloadLogger)
		{
			_httpClientFactory = httpClientFactory;
			_downloadAndCleanUp = downloadAndCleanUp;
			_downloadLogger = downloadLogger;
		}

		public async Task<bool> DownloadLatestSoftwareAsync (string downloadPath)
		{
			try
			{
				var httpClient = _httpClientFactory.CreateClient ();

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

				await _downloadAndCleanUp.DownloadSoftwareAndCleanUp (downloadPath, ieWebDriverFromSeleniumUrl);

				// Prepare to read the version of the IE Web Driver from Selenium's download page.
				var version = string.Empty;
				var ieDriverPath = Path.Combine (downloadPath, "IEDriverServer.exe");

				if (File.Exists (ieDriverPath))
				{
					// Read file version.
					var versionInfo = FileVersionInfo.GetVersionInfo (ieDriverPath);
					version = versionInfo.FileVersion;
				}

				await _downloadLogger
					.LogWebDriverInfo (
						DownloadsSoftware,
						downloadPath,
						version,
						ieWebDriverFromSeleniumUrl);

				return true;

			}
			catch (Exception ex)
			{
				Console.WriteLine ($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
				return false;
			}
		}
	}
}