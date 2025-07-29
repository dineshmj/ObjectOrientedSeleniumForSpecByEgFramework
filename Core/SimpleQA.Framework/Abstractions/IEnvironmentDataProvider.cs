using SimpleQA.Framework.Entities;

namespace SimpleQA.Framework.Abstractions
{
	// Provides environment-specific data for page navigation.
	//
	// TUserRole is an enumeration representing user roles.
	// TEnvironment is an enumeration representing the execution environments such as DEV, SIT, STG, UAT, etc.
	public interface IEnvironmentDataProvider<TUserRole, TEnvironment>
		where TUserRole : Enum
		where TEnvironment : Enum
	{
		// Preferred browser.
		WebBrowser GetPreferredWebBrowser ();

		// Eexecution environment (such as, FT, SIT, UAT, Pre-Prod, etc.)
		TEnvironment GetExecutionEnvironment ();

		// Web application's URL.
		string GetApplicationUrlFor (TEnvironment env);

		// Gets a dictionary of roles vs. credentials for the specified execution environment.
		IDictionary<TUserRole, Credential> GetCredentialsFor (TEnvironment env);

		// Sets the roles vs. credentials dictionary for the specified execution environment.
		void SetCredentialsFor (TEnvironment env, IDictionary<TUserRole, Credential> roleCredentialsDictionary);
	}
}