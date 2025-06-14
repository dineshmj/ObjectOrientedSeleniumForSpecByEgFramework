﻿using System.Text.Json;
using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.WebBrowsers.Chrome
{
	public sealed class ChromeWebDriverDownloadManager
		: ISoftwareDownloadManager
	{
		private const string CHROME_WEB_DRIVER_JSON_URL = "https://googlechromelabs.github.io/chrome-for-testing/last-known-good-versions-with-downloads.json";

		private readonly IHttpClientFactory httpClientFactory;
		private readonly ISoftwareDownloadLogger downloadLogger;
		private readonly IDownloadAndCleanUpManager downloadAndCleanUp;

		public Software DownloadsSoftware => Software.GoogleChromeWebDriver;

		public ChromeWebDriverDownloadManager (
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
				var jsonResponse = await httpClient.GetStringAsync (CHROME_WEB_DRIVER_JSON_URL);

				var parsedData
					= JsonSerializer
						.Deserialize<ChromeWebDriverJson> (
							jsonResponse,
							new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
						);

				if (parsedData?.Channels?.Stable == null)
				{
					return false;
				}

				var latestStable = parsedData.Channels.Stable;
				var chromeWebDriverUrl = latestStable?.Downloads?.ChromeDriver?.FirstOrDefault (d => d.Platform == "win64")?.Url;

				if (string.IsNullOrEmpty (chromeWebDriverUrl))
				{
					return false;
				}

				await this.downloadAndCleanUp.DownloadSoftwareAndCleanUp (downloadPath, chromeWebDriverUrl);
				await this.downloadLogger
					.LogWebDriverInfo (
						DownloadsSoftware,
						downloadPath,
						latestStable?.Version,
						chromeWebDriverUrl);

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