using HtmlAgilityPack;

using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.SeleniumGridHubJAR
{
	public sealed class SeleniumGridHubJarFileDownloadManager
		: ISoftwareDownloadManager
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IDownloadAndCleanUpManager _downloadAndCleanUp;
		private readonly ISoftwareDownloadLogger _downloadLogger;
		private const string SeleniumBaseUrl = "https://www.selenium.dev/downloads/";

		public Software DownloadsSoftware => Software.SeleniumGridHubJarFile;

		public SeleniumGridHubJarFileDownloadManager (
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
				string pageContent = await httpClient.GetStringAsync (SeleniumBaseUrl);

				// Prepare to scrape the Selenium Downloads page.
				var htmlPage = new HtmlDocument ();
				htmlPage.LoadHtml (pageContent);

				// Locate the Anchor tag for downloading the JAR file.
				var seleniumHubJarFileDownloadUrl
					= htmlPage
						.DocumentNode
							.SelectSingleNode ("//a[starts-with(@href, 'https://github.com/SeleniumHQ/selenium/releases/download/') and contains(@href, 'selenium-server-')]")
							?.GetAttributeValue ("href", string.Empty);

				if (string.IsNullOrEmpty (seleniumHubJarFileDownloadUrl))
				{
					return false;
				}

				var fullDownloadUrl = seleniumHubJarFileDownloadUrl.StartsWith ("http") ? seleniumHubJarFileDownloadUrl : "https://www.selenium.dev" + seleniumHubJarFileDownloadUrl;

				await _downloadAndCleanUp.DownloadSoftwareAndCleanUp (downloadPath, fullDownloadUrl, false);

				var seleniumHubJarFileVersion = string.Empty;
				var lastForwardSlashLocation = fullDownloadUrl.LastIndexOf ("/");

				if (lastForwardSlashLocation != -1)
				{
					var jarFileName = fullDownloadUrl.Substring (lastForwardSlashLocation +1);
					seleniumHubJarFileVersion = jarFileName.Replace ("selenium-server-", string.Empty).Replace (".jar", string.Empty);	
				}

				await _downloadLogger
					.LogWebDriverInfo (
						this.DownloadsSoftware,
						downloadPath,
						seleniumHubJarFileVersion,
						fullDownloadUrl);

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