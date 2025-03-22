using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.Abstractions
{
	// Provides environment-specific data for the web UI tests.
	//
	// TUserRole is an enumeration representing user roles.
	// TTestEnvironment is an enumeration representing the execution environments such as SIT, STG, etc.
	public interface IExecutionEnvironmentDataProvider<TUserRole, TExecutionEnvironment>
	{
		// Gets the preferred web browser for the web UI tests.
		WebBrowser GetPreferredWebBrowser ();

		// Gets the execution environment (such as, FT, SIT, UAT, Pre-Prod, etc.)
		TExecutionEnvironment GetExecutionEnvironment ();

		// Gets the web application's URL for the given execution environment.
		string GetApplicationBaseUrlFor (TExecutionEnvironment executionEnv);

		// Gets a dictionary of user credentials for various user roles, for the specified
		// execution environment.
		IDictionary<TUserRole, Credential> GetCredentialsFor (TExecutionEnvironment executionEnv);
	}
}