namespace OOSelenium.Utilities.Entities.Abstractions
{
    public interface ISoftwareDownloadManager
    {
		Software DownloadsSoftware { get; }

		Task<bool> DownloadLatestSoftwareAsync (string downloadPath);
	}
}