using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.Downloads
{
	public sealed class SoftwareDownloadLogger
		: ISoftwareDownloadLogger
	{
		public async Task LogWebDriverInfo (Software software, string downloadPath, string? version, string downloadUrl)
		{
			string logFilePath = Path.Combine (downloadPath, "SoftwareDownloadInfo.txt");
			string timestamp = DateTime.Now.ToString ("dd-MMM-yyyy hh:mm:ss tt, zzz");

			string logEntry =
	$@"Software = {software}
Version = {version}
Download URL = {downloadUrl}
Downloaded on = {timestamp}

";
			if (File.Exists (logFilePath))
			{
				string existingContent = await File.ReadAllTextAsync (logFilePath);
				await File.WriteAllTextAsync (logFilePath, logEntry + existingContent);
			}
			else
			{
				await File.WriteAllTextAsync (logFilePath, logEntry);
			}
		}
	}
}