using SimpleQA.Tools.Selenium.Updater.Entities;

namespace SimpleQA.Tools.Selenium.Updater.Abstractions
{
    public interface ISoftwareDownloadLogger
    {
		Task LogWebDriverInfo (Software software, string downloadPath, string? version, string downloadUrl);
	}
}