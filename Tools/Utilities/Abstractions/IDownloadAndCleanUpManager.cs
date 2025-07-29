namespace SimpleQA.Tools.Selenium.Updater.Abstractions
{
    public interface IDownloadAndCleanUpManager
    {
        Task DownloadSoftwareAndCleanUp (string downloadPath, string softwareUrl, bool unzippingRequired = true);
	}
}