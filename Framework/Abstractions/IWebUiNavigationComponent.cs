namespace OOSelenium.Framework.Abstractions
{
	public interface IWebUiNavigationComponent<TUserRole, TExecutionEnvironment>
		: IDisposable
	{
		IDecryptor Decryptor { get; }

		IExecutionEnvironmentPageDataProvider<TUserRole, TExecutionEnvironment> ExecutionEnvironmentPageDataProvider { get; }
	}
}