namespace SimpleQA.Tools.Selenium.Updater.Entities.Chrome
{
    public sealed class ChromeWebDriverVersion
    {
		public string? Version { get; set; }

		public string? Channel { get; set; }

		public ChromeWebDriverDownload? Downloads { get; set; }
	}
}