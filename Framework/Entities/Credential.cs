namespace OOSelenium.Framework.Entities
{
	public sealed class Credential
	{
		public string UserId { get; set; }

		public string EncryptedPassword { get; set; }

		public string AdditionalInfoOneIfAny { get; set; }

		public string AdditionalInfoTwoIfAny { get; set; }
	}
}