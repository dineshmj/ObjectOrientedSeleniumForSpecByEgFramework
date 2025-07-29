using OOSelenium.Utilities.Entities.Config;

namespace OOSelenium.Utilities.Entities.Abstractions
{
    public interface IConfigManager
    {
		string ReadLastUsedPath (ref AppSettings? appSettings, Software softwareToDownload);

		void UpdateLastUsedPath (AppSettings? appSettings, string path, Software webBrowser);
	}
}