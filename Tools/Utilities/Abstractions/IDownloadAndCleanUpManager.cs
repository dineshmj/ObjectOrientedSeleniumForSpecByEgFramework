namespace OOSelenium.Utilities.Entities.Abstractions
{
    public interface IDownloadAndCleanUpManager
    {
        Task DownloadSoftwareAndCleanUp (string downloadPath, string softwareUrl, bool unzippingRequired = true);
	}
}