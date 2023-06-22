namespace OOSelenium.Framework.Abstractions
{
	public interface IBusinessFunctionFlowComponent<TUserRole, TTestEnvironment>
		: IDisposable
	{
		IDecryptor Decryptor { get; }

		ITestBackgroundDataProvider<TUserRole, TTestEnvironment>TestBackgroundDataProvider { get; }
	}
}