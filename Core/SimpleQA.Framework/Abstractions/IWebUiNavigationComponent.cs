namespace SimpleQA.Framework.Abstractions
{
	public interface IWebUiNavigationComponent<TUserRole, TEnvironment>
		: IDisposable
		where TUserRole : Enum
		where TEnvironment : Enum
	{
		IDecryptor Decryptor { get; }

		IEnvironmentDataProvider<TUserRole, TEnvironment> EnvironmentDataProvider { get; }
	}
}