using System.ComponentModel;

using SimpleQA.TestWebApp.Business.Entities;

namespace SimpleQA.TestWebApp.Models
{
	/// <summary>
	/// Represents signing in credentials.
	/// </summary>
	public sealed class SignInModel
	{
		[DisplayName ("User ID")]
		public string UserId { get; set; }

		[DisplayName("Password")]
		public string EncryptedPassword { get; set; }

		[DisplayName ("User Role")]
		public UserRole UserRole { get; set; }
	}
}