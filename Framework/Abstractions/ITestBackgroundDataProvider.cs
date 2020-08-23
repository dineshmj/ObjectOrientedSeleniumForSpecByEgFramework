using System.Collections.Generic;

using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.Abstractions
{
	/// <summary>
	/// Provides data for the web UI acceptance tests.
	/// </summary>
	/// <typeparam name="TUserRole">An enumeration representing user roles.</typeparam>
	/// <typeparam name="TTestEnvironment">An enumeration representing the test environments.</typeparam>
	public interface ITestBackgroundDataProvider<TUserRole, TTestEnvironment>
	{
		/// <summary>
		/// Gets the web browser that is to be used for the web UI acceptance tests.
		/// </summary>
		WebBrowser GetWebBrowserTypeToUseForAcceptanceTests ();

		/// <summary>
		/// Gets the target test environment (such as, FT, SIT, UAT, Pre-Prod, etc.)
		/// </summary>
		TTestEnvironment GetTargetTestEnvironment ();

		/// <summary>
		/// Gets the target web application's URL for the specified test environment.
		/// </summary>
		string GetTargetApplicationBaseUrlFor (TTestEnvironment testEnv);

		/// <summary>
		/// Gets a dictionary of valid credentials for various user roles for the specified
		/// test environment.
		/// </summary>
		IDictionary<TUserRole, Credential> GetValidCredentialsOfUserTypesFor (TTestEnvironment testEnv);
	}
}
