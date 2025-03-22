using OOSelenium.Framework.Abstractions;

namespace SampleWebApp.UiTests.LoginTests.Background
{
	public sealed class PassThroughDecryptor
		: IDecryptor
	{
		public string Decrypt (string cipherText)
		{
			// TODO: Please implement your decryption logic here.
			// TODO: As of now, for demonstration purpose, the same cipher text is returned.
			var legibleText = cipherText;

			return legibleText;
		}
	}
}