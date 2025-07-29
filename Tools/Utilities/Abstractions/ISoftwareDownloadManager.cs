using SimpleQA.Tools.Selenium.Updater.Entities;

namespace SimpleQA.Tools.Selenium.Updater.Abstractions
{
    public interface ISoftwareDownloadManager
    {
		Software DownloadsSoftware { get; }

		Task<bool> DownloadLatestSoftwareAsync (string downloadPath);
	}
}