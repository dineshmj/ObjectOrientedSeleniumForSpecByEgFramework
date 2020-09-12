namespace OOSelenium.Framework.Entities
{
	public sealed class Credential
	{
		public string UserId { get; private set; }

		public string EncryptedPassword { get; private set; }

		public string AdditionalInfoOneIfAny { get; private set; }

		public string AdditionalInfoTwoIfAny { get; private set; }

		public Credential
			(
				string userId,
				string encryptedPassword,
				string additionalInfoOneIfAny = "",
				string additionalInfoTwoIfAny = ""
			)
		{
			this.UserId = userId;
			this.EncryptedPassword = encryptedPassword;
			this.AdditionalInfoOneIfAny = additionalInfoOneIfAny;
			this.AdditionalInfoTwoIfAny = additionalInfoTwoIfAny;
		}
	}
}