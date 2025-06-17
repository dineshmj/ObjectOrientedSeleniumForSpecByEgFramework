using OOSelenium.Framework.Abstractions;

namespace GitHubServerSearch.Background
{
	public sealed class PassThroughDecrypter
		: IDecryptor
	{
		public string Decrypt (string cipherText)
		{
			var legibleText = cipherText;
			return legibleText;
		}
	}
}