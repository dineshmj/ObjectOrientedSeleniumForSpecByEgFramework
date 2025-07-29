using System.Text.Json;

using SimpleQA.Tools.Selenium.Updater.Abstractions;
using SimpleQA.Tools.Selenium.Updater.Entities;
using SimpleQA.Tools.Selenium.Updater.Entities.Config;

namespace SimpleQA.Tools.Selenium.Updater.Services
{
	public sealed class ConfigManager
		: IConfigManager
	{
		private const string CONFIG_FILE_NAME = "appSettings.json";

		public string? ReadLastUsedPath (ref AppSettings appSettings, Software softwareToDownload)
		{
			if (File.Exists (CONFIG_FILE_NAME))
			{
				var applicationExePath = AppDomain.CurrentDomain.BaseDirectory;

				var json = File.ReadAllText (CONFIG_FILE_NAME);
				appSettings = JsonSerializer.Deserialize<AppSettings> (json);

				var relativePath
					= softwareToDownload switch
					{
						Software.MicrosoftEdgeWebDriver => appSettings.LastUsedEdgeDriverFolderRelativeToThisExePath,
						Software.GoogleChromeWebDriver => appSettings.LastUsedChromeDriverFolderRelativeToThisExePath,
						Software.MozillaFirefoxWebDriver => appSettings.LastUsedFirefoxDriverFolderRelativeToThisExePath,
						Software.InternetExplorerWebDriver => appSettings.LastUsedIeDriverFolderRelativeToThisExePath,
						Software.SeleniumGridHubJarFile => appSettings.LastUsedSeleniumHubJarFileFolderRelativeToThisExePath,
						_ => throw new NotSupportedException ($"Unrecognized software '{softwareToDownload}'")
					};

				return
					Path.GetFullPath (Path.Combine (applicationExePath, relativePath));
			}

			throw new FileNotFoundException (CONFIG_FILE_NAME + " not found. Please ensure the file exists and is properly formatted.");
		}

		public void UpdateLastUsedPath (AppSettings appSettings, string downloadPath, Software softwareToDownload)
		{
			var applicationExePath = AppDomain.CurrentDomain.BaseDirectory;
			var relativePath = Path.GetRelativePath (applicationExePath, downloadPath);

			switch (softwareToDownload)
			{
				case Software.MicrosoftEdgeWebDriver:
					appSettings.LastUsedEdgeDriverFolderRelativeToThisExePath = relativePath;
					break;

				case Software.GoogleChromeWebDriver:
					appSettings.LastUsedChromeDriverFolderRelativeToThisExePath = relativePath;
					break;

				case Software.MozillaFirefoxWebDriver:
					appSettings.LastUsedFirefoxDriverFolderRelativeToThisExePath = relativePath;
					break;

				case Software.InternetExplorerWebDriver:
					appSettings.LastUsedIeDriverFolderRelativeToThisExePath = relativePath;
					break;

				case Software.SeleniumGridHubJarFile:
					appSettings.LastUsedSeleniumHubJarFileFolderRelativeToThisExePath = relativePath;
					break;

				default:
					throw new NotSupportedException ($"Unrecognized software '{softwareToDownload}'");
			}

			var json = JsonSerializer.Serialize (appSettings, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText (CONFIG_FILE_NAME, json);
		}
	}
}