using SimpleQA.Framework.Abstractions;

namespace SimpleQA.Framework.Services
{
	public sealed class PassThroughDecryptor
		: IDecryptor
	{
		public string Decrypt (string cipherText)
		{
			// NOTE: This is a pass-through decryptor that does not perform any actual decryption.
			// NOTE: This is useful for testing purposes or when no encryption is needed.
			var legibleText = cipherText;

			return legibleText;
		}
	}
}