using SimpleQA.Framework.Abstractions;

namespace SimpleQA.UseCases.Selenium.GitHubServerSearch.Background
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