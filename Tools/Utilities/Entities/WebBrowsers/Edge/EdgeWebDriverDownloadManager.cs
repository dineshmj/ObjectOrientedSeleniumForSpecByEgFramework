using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.WebBrowsers.Edge
{
	public sealed class EdgeWebDriverDownloadManager
		: ISoftwareDownloadManager
	{
		private readonly IDownloadAndCleanUpManager downloadAndCleanUp;
		private readonly ISoftwareDownloadLogger downloadLogger;

		public Software DownloadsSoftware => Software.MicrosoftEdgeWebDriver;

		public EdgeWebDriverDownloadManager (
			IDownloadAndCleanUpManager downloadAndCleanUp,
			ISoftwareDownloadLogger downloadLogger)
		{
			this.downloadAndCleanUp = downloadAndCleanUp;
			this.downloadLogger = downloadLogger;
		}

		public async Task<bool> DownloadLatestSoftwareAsync (string downloadPath)
		{
			try
			{
				// Retrieve the installed Edge browser version
				var edgeVersion = GetInstalledEdgeVersion ();

				if (string.IsNullOrEmpty (edgeVersion))
				{
					Console.WriteLine ("Microsoft Edge is not installed on this system.");
					return false;
				}

				// Construct the WebDriver download URL
				var edgeWebDriverUrl = $"https://msedgedriver.azureedge.net/{edgeVersion}/edgedriver_win64.zip";

				// Download the edge web driver.
				await this.downloadAndCleanUp.DownloadSoftwareAndCleanUp (downloadPath, edgeWebDriverUrl);
				await this.downloadLogger
					.LogWebDriverInfo (
						DownloadsSoftware,
						downloadPath,
						edgeVersion,
						edgeWebDriverUrl);

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine ($"Error while downloading latest web driver software.\r\n\r\nMessage: {ex.Message}\r\n\r\nStackTrace: {ex.StackTrace}");
				return false;
			}
		}

		#region Private methods.

		private string GetInstalledEdgeVersion ()
		{
			try
			{
				string [] possiblePaths = [
					@"SOFTWARE\Microsoft\Edge\BLBeacon", // HKLM
					@"SOFTWARE\WOW6432Node\Microsoft\Edge\BLBeacon", // HKLM 32-bit on 64-bit OS
					@"Software\Microsoft\Edge\BLBeacon" // HKCU
				];

				foreach (var path in possiblePaths)
				{
					using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (path) ??
									 Microsoft.Win32.Registry.CurrentUser.OpenSubKey (path))
					{
						var edgeVersion = key?.GetValue ("version")?.ToString ();

						if (!string.IsNullOrEmpty (edgeVersion))
						{
							return edgeVersion;
						}
					}
				}
			}
			catch
			{
				return null;
			}

			return null;
		}

		#endregion
	}
}