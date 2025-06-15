using System.Text.Json;
using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.WebBrowsers.Firefox
{
	public sealed class FirefoxWebDriverDownloadManager
		: ISoftwareDownloadManager
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly IDownloadAndCleanUpManager downloadAndCleanUp;
		private readonly ISoftwareDownloadLogger downloadLogger;

		private const string FIREFOX_WEB_DRIVER_PAGE_URL = "https://api.github.com/repos/mozilla/geckodriver/releases/latest";

		public Software DownloadsSoftware => Software.MozillaFirefoxWebDriver;

		public FirefoxWebDriverDownloadManager (
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
				httpClient.DefaultRequestHeaders.UserAgent.ParseAdd ("Mozilla/5.0");

				var jsonResponse = await httpClient.GetStringAsync (FIREFOX_WEB_DRIVER_PAGE_URL);
				var parsedData = JsonSerializer.Deserialize<JsonElement> (jsonResponse);
				var firefoxVersion = parsedData.GetProperty ("tag_name").GetString ();
				var firefoxWebDriverUrl
					= parsedData
						.GetProperty ("assets")
						.EnumerateArray ()
						.First (a =>
							a.GetProperty ("name")
								.GetString ()
								.Contains ("win64.zip"))
						.GetProperty ("browser_download_url").GetString ();

				await this.downloadAndCleanUp.DownloadSoftwareAndCleanUp (downloadPath, firefoxWebDriverUrl);
				await this.downloadLogger
					.LogWebDriverInfo (
						DownloadsSoftware,
						downloadPath,
						firefoxVersion,
						firefoxWebDriverUrl);

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