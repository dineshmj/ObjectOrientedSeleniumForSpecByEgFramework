using Microsoft.Extensions.DependencyInjection;

using OOSelenium.Utilities.Entities;
using OOSelenium.Utilities.Entities.Abstractions;
using OOSelenium.Utilities.Entities.Config;
using OOSelenium.Utilities.Entities.Downloads;
using OOSelenium.Utilities.Entities.SeleniumGridHubJAR;
using OOSelenium.Utilities.Entities.WebBrowsers.Chrome;
using OOSelenium.Utilities.Entities.WebBrowsers.Edge;
using OOSelenium.Utilities.Entities.WebBrowsers.Firefox;
using OOSelenium.Utilities.Entities.WebBrowsers.IE;

public class Program
{
	private static IConfigManager? configManager;
	private static AppSettings? appSettings;

	public static async Task Main ()
	{
		var serviceProvider
			= new ServiceCollection ()
				.AddHttpClient ()
				.AddSingleton<IConfigManager, ConfigManager> ()
				.AddSingleton<IDownloadAndCleanUpManager, DownloadAndCleanUpManager> ()
				.AddSingleton<ISoftwareDownloadLogger, SoftwareDownloadLogger> ()
				.AddSingleton<ISoftwareDownloadManager, EdgeWebDriverDownloadManager> ()
				.AddSingleton<ISoftwareDownloadManager, ChromeWebDriverDownloadManager> ()
				.AddSingleton<ISoftwareDownloadManager, FirefoxWebDriverDownloadManager> ()
				.AddSingleton<ISoftwareDownloadManager, InternetExplorerWebDriverDownloadManager> ()
				.AddSingleton<ISoftwareDownloadManager, SeleniumGridHubJarFileDownloadManager> ()
			.BuildServiceProvider ();

		// Get download managers for all types of downloads.
		var downloadManagers = serviceProvider.GetRequiredService<IEnumerable<ISoftwareDownloadManager>> ();

		// Read the configuration for various download directories.
		configManager = serviceProvider.GetRequiredService<IConfigManager> ();

		// Work with each software download manager.
		foreach (var oneDownloadManager in downloadManagers)
		{
			// Get download path and type of download.
			var softwareToDownload = oneDownloadManager.DownloadsSoftware;
			var downloadPath = Program.GetDownloadPathFromUser (softwareToDownload);

			// Download and tidy-up the download folder.
			var success = await oneDownloadManager.DownloadLatestSoftwareAsync (downloadPath);

			if (success)
			{
				Console.WriteLine ($"\r\nSoftware '{softwareToDownload}' downloaded successfully!\r\n");

				// Update the Configuration source such that last specified download folder for the software type is saved.
				configManager.UpdateLastUsedPath (Program.appSettings, downloadPath, softwareToDownload);
			}
			else
			{
				Console.WriteLine ($"Failed to download software '{softwareToDownload}'.");
			}
		}

		Console.WriteLine ("All web drivers for the Web Browsers have been downloaded.\r\n\r\nPress any key to exit...");
		Console.ReadKey ();
	}

	private static string GetDownloadPathFromUser (Software softwareToDownload)
	{
		// Get the folder path where it was downloaded for this software.
		var lastUsedPath = configManager?.ReadLastUsedPath (ref Program.appSettings, softwareToDownload);

		Console.Write ($"Enter download path for '{ softwareToDownload }' [{lastUsedPath}]: ");
		var inputPath = Console.ReadLine ()?.Trim ();

		var finalPath = string.IsNullOrWhiteSpace (inputPath) ? lastUsedPath : inputPath;

		if (!Directory.Exists (finalPath))
		{
			Directory.CreateDirectory (finalPath);
		}

		return finalPath;
	}
}