using SimpleQA.Tools.Selenium.Updater.Entities;
using SimpleQA.Tools.Selenium.Updater.Entities.Config;

namespace SimpleQA.Tools.Selenium.Updater.Abstractions
{
    public interface IConfigManager
    {
		string ReadLastUsedPath (ref AppSettings? appSettings, Software softwareToDownload);

		void UpdateLastUsedPath (AppSettings? appSettings, string path, Software webBrowser);
	}
}