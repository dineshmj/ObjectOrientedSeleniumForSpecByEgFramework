using System.Text.Json;

using OOSelenium.Utilities.Entities.Config;

namespace OOSelenium.Utilities.Entities.Abstractions
{
	public sealed class ConfigManager
		: IConfigManager
	{
		private const string CONFIG_FILE_NAME = "appSettings.json";

		public string? ReadLastUsedPath (ref AppSettings appSettings, Software softwareToDownload)
		{
			var currentDirectory = Directory.GetCurrentDirectory ();

			if (File.Exists (CONFIG_FILE_NAME))
			{
				try
				{
					var json = File.ReadAllText (CONFIG_FILE_NAME);
					appSettings = JsonSerializer.Deserialize<AppSettings> (json);

					return
						softwareToDownload switch
						{
							Software.MicrosoftEdgeWebDriver => appSettings.LastUsedEdgeDriverFolderPath,
							Software.GoogleChromeWebDriver => appSettings.LastUsedChromeDriverFolderPath,
							Software.MozillaFirefoxWebDriver => appSettings.LastUsedFirefoxDriverFolderPath,
							Software.InternetExplorerWebDriver => appSettings.LastUsedIeDriverFolderPath,
							Software.SeleniumGridHubJarFile => appSettings.LastUsedSeleniumHubJarFileFolderPath,
							_ => throw new NotSupportedException ($"Unrecognized software '{softwareToDownload}'")
						};
				}
				catch
				{
					return currentDirectory;
				}
			}

			return currentDirectory;
		}

		public void UpdateLastUsedPath (AppSettings appSettings, string downloadPath, Software softwareToDownload)
		{
			switch (softwareToDownload)
			{
				case Software.MicrosoftEdgeWebDriver:
					appSettings.LastUsedEdgeDriverFolderPath = downloadPath;
					break;

				case Software.GoogleChromeWebDriver:
					appSettings.LastUsedChromeDriverFolderPath = downloadPath;
					break;

				case Software.MozillaFirefoxWebDriver:
					appSettings.LastUsedFirefoxDriverFolderPath = downloadPath;
					break;

				case Software.InternetExplorerWebDriver:
					appSettings.LastUsedIeDriverFolderPath = downloadPath;
					break;

				case Software.SeleniumGridHubJarFile:
					appSettings.LastUsedSeleniumHubJarFileFolderPath = downloadPath;
					break;

				default:
					throw new NotSupportedException ($"Unrecognized software '{softwareToDownload}'");
			}

			var json = JsonSerializer.Serialize (appSettings, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText (CONFIG_FILE_NAME, json);
		}
	}
}