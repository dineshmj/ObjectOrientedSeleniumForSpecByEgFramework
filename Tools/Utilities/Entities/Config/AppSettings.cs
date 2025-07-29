namespace SimpleQA.Tools.Selenium.Updater.Entities.Config
{
    public sealed class AppSettings
    {
		public string? LastUsedEdgeDriverFolderRelativeToThisExePath { get; set; }

		public string? LastUsedChromeDriverFolderRelativeToThisExePath { get; set; }
		
		public string? LastUsedFirefoxDriverFolderRelativeToThisExePath { get; set; }

		public string? LastUsedIeDriverFolderRelativeToThisExePath { get; set; }

		public string? LastUsedSeleniumHubJarFileFolderRelativeToThisExePath { get; set; }
	}
}