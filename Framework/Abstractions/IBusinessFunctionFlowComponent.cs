using System;

namespace OOSelenium.Framework.Abstractions
{
	public interface IBusinessFunctionFlowComponent<TUserRole, TTestEnvironment>
		: IDisposable
	{
		ITestBackgroundDataProvider<TUserRole, TTestEnvironment>TestBackgroundDataProvider { get; }
	}
}