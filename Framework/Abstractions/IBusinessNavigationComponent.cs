namespace OOSelenium.Framework.Abstractions
{
	public interface IBusinessNavigationComponent<TUserRole, TExecutionEnvironment>
		: IDisposable
	{
		IDecryptor Decryptor { get; }

		IExecutionEnvironmentDataProvider<TUserRole, TExecutionEnvironment>ExecutionEnvironmentDataProvider { get; }
	}
}