using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.Downloads
{
	public sealed class SoftwareDownloadLogger
		: ISoftwareDownloadLogger
	{
		public async Task LogWebDriverInfo (Software software, string downloadPath, string? version, string downloadUrl)
		{
			var logFilePath = Path.Combine (downloadPath, "SoftwareDownloadInfo.txt");
			var timestamp = DateTime.Now.ToString ("dd-MMM-yyyy hh:mm:ss tt, zzz");

			var logEntry =
	$@"Software = {software}
Version = {version}
Download URL = {downloadUrl}
Downloaded on = {timestamp}

";
			if (File.Exists (logFilePath))
			{
				var existingContent = await File.ReadAllTextAsync (logFilePath);
				await File.WriteAllTextAsync (logFilePath, logEntry + existingContent);
			}
			else
			{
				await File.WriteAllTextAsync (logFilePath, logEntry);
			}
		}
	}
}