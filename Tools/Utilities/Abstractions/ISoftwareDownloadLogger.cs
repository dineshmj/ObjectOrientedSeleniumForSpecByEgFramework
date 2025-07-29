namespace OOSelenium.Utilities.Entities.Abstractions
{
    public interface ISoftwareDownloadLogger
    {
		Task LogWebDriverInfo (Software software, string downloadPath, string? version, string downloadUrl);
	}
}