using System.IO.Compression;

using OOSelenium.Utilities.Entities.Abstractions;

namespace OOSelenium.Utilities.Entities.Downloads
{
	public sealed class DownloadAndCleanUpManager
		: IDownloadAndCleanUpManager
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public DownloadAndCleanUpManager (IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task DownloadSoftwareAndCleanUp (string downloadPath, string softwareUrl, bool unzippingRequired = true)
		{
			// Archive folder.
			var archiveFolderPath = Path.Combine (downloadPath, LookUp.ARCHIVE_FOLDER_NAME);

			if (Directory.Exists (archiveFolderPath) == false)
			{
				Directory.CreateDirectory (archiveFolderPath);
			}

			// Timestamp for archiving.
			string timestamp = DateTime.Now.ToString (LookUp.TIME_STAMP_FORMAT);

			// Sub-folders in the download path that are to be archived.
			var subFolders
				= Directory
					.GetDirectories (downloadPath)
					.Except ([
						Path.Combine (downloadPath, LookUp.ARCHIVE_FOLDER_NAME)
						]);

			// Files in the download path that are to be archived.
			var files
				= Directory
					.GetFiles (downloadPath)
					.Except([
						Path.Combine (downloadPath, LookUp.README_TXT_FILE_NAME),
						Path.Combine (downloadPath, LookUp.DOWNLOAD_LOG_FILE_NAME),
						Path.Combine (downloadPath, LookUp.START_SELENIUM_GRID_SERVER_CMD_FILE_NAME),
						Path.Combine (downloadPath, LookUp.START_CHROME_NODE_FILE_NAME),
						Path.Combine (downloadPath, LookUp.START_FIREFOX_NODE_FILE_NAME)
						]);

			// Move each file in the download folder to the archive folder.
			foreach (var file in files)
			{
				var destinationPath = Path.Combine (archiveFolderPath, $"{Path.GetFileNameWithoutExtension (file)}_{timestamp}{Path.GetExtension (file)}");
				File.Move (file, destinationPath);
			}

			// Move each sub-folder in the download folder to the archive folder.
			foreach (var folder in subFolders)
			{
				var folderParts = folder.Split ('\\');
				var folderToMove = folderParts [folderParts.Length - 1];

				var destinationPath = Path.Combine (archiveFolderPath, $"{folderToMove}_{timestamp}");
				Directory.Move (folder, destinationPath);
			}

			// Get the download file name (the last segment in the download URL).
			var softwareUrlSegments = softwareUrl.Split ('/');
			var targetDownloadFile =  softwareUrlSegments [softwareUrlSegments.Length - 1];

			// Prepare a filename from it.
			var targetDownloadFilePath = Path.Combine (downloadPath, targetDownloadFile);

			var httpClient = _httpClientFactory.CreateClient ();

			using (var stream = await httpClient.GetStreamAsync (softwareUrl))
			using (var fileStream = new FileStream (targetDownloadFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				// Write the download file to OS folder.
				await stream.CopyToAsync (fileStream);
			}

			// Unzip the contents, if required.
			if (unzippingRequired)
			{
				ZipFile.ExtractToDirectory (targetDownloadFilePath, downloadPath, true);
				File.Delete (targetDownloadFilePath);		// Delete the ZIP file.
			}

			// If zipping causes sub-folders to be created, move contents from them to the download folder.
			var subDirectories
				= Directory
					.GetDirectories (downloadPath)
					.Except ([
						Path.Combine (downloadPath, LookUp.ARCHIVE_FOLDER_NAME)
						]);

			// Move each sub-folder contents to the download path.
			foreach (var subDirectory in subDirectories)
			{
				var subDirectoryFiles = Directory.GetFiles (subDirectory);
				foreach (var subDirectoryFile in subDirectoryFiles)
				{
					// Move to download path.
					var fileName = Path.GetFileName (subDirectoryFile);
					var destinationPath = Path.Combine (downloadPath, fileName);
					File.Move (subDirectoryFile, destinationPath);
				}

				// Delete the downloaded file, as you have already unzipped them.
				Directory.Delete (subDirectory);
			}
		}
	}
}