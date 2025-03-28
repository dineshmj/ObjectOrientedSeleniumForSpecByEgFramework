using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace GitHubServerSearch.Background
{
	public sealed class GitHubPageDataProvider
		: IExecutionEnvironmentPageDataProvider<UserRole, ExecutionEnvironment>
	{
		private IDictionary<ExecutionEnvironment, IDictionary<UserRole, Credential>> devEnvRoleAndCredentialsDictionary;

		public string GetWebApplicationUrlFor (ExecutionEnvironment executionEnv)
		{
			switch (executionEnv)
			{
				case ExecutionEnvironment.Development:
					return URLs.URL_GIT_HUB_LOGIN_PAGE;

				default:
					throw new NotImplementedException ($"Execution Environment, \"{executionEnv}\" is not yet implemented.");
			}
		}

		public IDictionary<UserRole, Credential> GetCredentialsFor (ExecutionEnvironment executionEnv)
		{
			switch (executionEnv)
			{
				case ExecutionEnvironment.Development:
					if (this.devEnvRoleAndCredentialsDictionary.ContainsKey (executionEnv))
					{
						return this.devEnvRoleAndCredentialsDictionary [executionEnv];
					}
					break;
			}

			throw new NotImplementedException ($"Execution Environment, \"{executionEnv}\" is not yet implemented.");
		}

		public void SetCredentialsFor (ExecutionEnvironment executionEnv, IDictionary<UserRole, Credential> roleCredentialsDictionary)
		{
			this.devEnvRoleAndCredentialsDictionary [executionEnv] = roleCredentialsDictionary;
		}

		public ExecutionEnvironment GetExecutionEnvironment ()
		{
			return ExecutionEnvironment.Development;
		}

		public WebBrowser GetPreferredWebBrowser ()
		{
			return WebBrowser.MicrosoftEdge;
		}
	}
}