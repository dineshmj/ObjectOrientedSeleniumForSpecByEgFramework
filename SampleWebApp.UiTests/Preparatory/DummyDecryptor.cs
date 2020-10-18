using OOSelenium.Framework.Abstractions;

namespace SampleWebApp.UiTests.Preparatory
{
	public sealed class DummyDecryptor
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