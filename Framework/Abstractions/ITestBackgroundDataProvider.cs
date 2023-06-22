using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.Abstractions
{
	//
	// Provides data for the web UI acceptance tests.
	//
	// TUserRole is an enumeration representing user roles.
	// TTestEnvironment is an enumeration representing the test environments.
	//
	public interface ITestBackgroundDataProvider<TUserRole, TTestEnvironment>
	{
		// Gets the web browser that is to be used for the web UI acceptance tests.
		WebBrowser GetWebBrowserTypeToUseForAcceptanceTests ();

		// Gets the target test environment (such as, FT, SIT, UAT, Pre-Prod, etc.)
		TTestEnvironment GetTargetTestEnvironment ();

		// Gets the target web application's URL for the specified test environment.
		string GetTargetApplicationBaseUrlFor (TTestEnvironment testEnv);

		//
		// Gets a dictionary of valid credentials for various user roles for the specified
		// test environment.
		//
		IDictionary<TUserRole, Credential> GetValidCredentialsOfUserTypesFor (TTestEnvironment testEnv);
	}
}