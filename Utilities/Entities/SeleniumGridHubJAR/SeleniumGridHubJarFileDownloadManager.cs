using HtmlAgilityPack;

using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.SeleniumGridHubJAR
{
	public sealed class SeleniumGridHubJarFileDownloadManager
		: ISoftwareDownloadManager
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly IDownloadAndCleanUpManager downloadAndCleanUp;
		private readonly ISoftwareDownloadLogger downloadLogger;
		private const string SeleniumBaseUrl = "https://www.selenium.dev/downloads/";

		public Software DownloadsSoftware => Software.SeleniumGridHubJarFile;

		public SeleniumGridHubJarFileDownloadManager (
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
				var pageContent = await httpClient.GetStringAsync (SeleniumBaseUrl);

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

				var fullDownloadUrl
					= seleniumHubJarFileDownloadUrl.StartsWith ("http")
						? seleniumHubJarFileDownloadUrl
						: "https://www.selenium.dev" + seleniumHubJarFileDownloadUrl;

				await this.downloadAndCleanUp.DownloadSoftwareAndCleanUp (downloadPath, fullDownloadUrl, false);

				var seleniumHubJarFileVersion = string.Empty;
				var lastForwardSlashLocation = fullDownloadUrl.LastIndexOf ("/");

				if (lastForwardSlashLocation != -1)
				{
					var jarFileName = fullDownloadUrl.Substring (lastForwardSlashLocation +1);
					seleniumHubJarFileVersion = jarFileName.Replace ("selenium-server-", string.Empty).Replace (".jar", string.Empty);	
				}

				await this.downloadLogger
					.LogWebDriverInfo (
						this.DownloadsSoftware,
						downloadPath,
						seleniumHubJarFileVersion,
						fullDownloadUrl);

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