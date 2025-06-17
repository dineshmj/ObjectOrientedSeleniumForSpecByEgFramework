using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.Abstractions
{
	// Provides environment-specific data for the web UI navigations.
	//
	// TUserRole is an enumeration representing user roles.
	// TExecutionEnvironment is an enumeration representing the execution environments such as SIT, STG, etc.
	public interface IExecutionEnvironmentPageDataProvider<TUserRole, TExecutionEnvironment>
	{
		// Preferred browser.
		WebBrowser GetPreferredWebBrowser ();

		// Eexecution environment (such as, FT, SIT, UAT, Pre-Prod, etc.)
		TExecutionEnvironment GetExecutionEnvironment ();

		// Web application's URL.
		string GetWebApplicationUrlFor (TExecutionEnvironment executionEnv);

		// Gets a dictionary of roles vs. credentials for the specified execution environment.
		IDictionary<TUserRole, Credential> GetCredentialsFor (TExecutionEnvironment executionEnv);

		// Sets the roles vs. credentials dictionary for the specified execution environment.
		void SetCredentialsFor (TExecutionEnvironment executionEnv, IDictionary<TUserRole, Credential> roleCredentialsDictionary);
	}
}