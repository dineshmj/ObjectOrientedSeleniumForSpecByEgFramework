using Microsoft.Extensions.Configuration;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Misc;
using SampleWebApp.UiTests.LoginTests.Background;

namespace SampleWebApp.UiTests.LoginTests.DataProviders
{
	public sealed class InsuranceOneTestDataProvider
		: IExecutionEnvironmentPageDataProvider<UserRole, ExecutionEnvironment>
	{
		private readonly IConfigurationRoot appSettings;
		private IDictionary<UserRole, Credential> functionalTestEnvRolesAndCredentialsDictionary
			= new Dictionary<UserRole, Credential>
				{
					{ UserRole.Admin, new Credential (userId: "admin", encryptedPassword: "123") },
					{ UserRole.QuoteIssuer, new Credential (userId: "quote_issuer1", encryptedPassword: "123")  },
					{ UserRole.ProposalInitiator, new Credential (userId: "proposer1", encryptedPassword: "123")  },
					{ UserRole.PolicyApprover, new Credential (userId: "policy_approver1", encryptedPassword: "123")  },
				};
		private IDictionary<ExecutionEnvironment, IDictionary<UserRole, Credential>> envRolesAndCredentialsDictionary;

		public InsuranceOneTestDataProvider ()
        {
			appSettings
				= new ConfigurationBuilder ()
					.AddJsonFile ("appsettings.json")
					.Build ();

			this.envRolesAndCredentialsDictionary
				= new Dictionary<ExecutionEnvironment, IDictionary<UserRole, Credential>>
					{
						{ ExecutionEnvironment.FunctionalTest, this.functionalTestEnvRolesAndCredentialsDictionary }
					};
		}

		public string GetWebApplicationUrlFor (ExecutionEnvironment executionEnv)
		{
			switch (executionEnv)
			{
				case ExecutionEnvironment.FunctionalTest:
					return "https://localhost:44399/";

				default:
					throw new NotImplementedException ($"Execution Environment, \" { executionEnv } \" is not yet implemented.");
			}
		}

		public ExecutionEnvironment GetExecutionEnvironment ()
		{
			// Modify the logic such that it is taken from the configuration file.
			return ExecutionEnvironment.FunctionalTest;
		}

		public IDictionary<UserRole, Credential> GetCredentialsFor (ExecutionEnvironment testEnv)
		{
			switch (testEnv)
			{
				case ExecutionEnvironment.FunctionalTest:
					return this.envRolesAndCredentialsDictionary [testEnv];

				default:
					throw new NotImplementedException ($"Test Environment, \" { testEnv } \" is not yet implemented.");
			}
		}

		public WebBrowser GetPreferredWebBrowser ()
		{
			// Modify the logic such that it is taken from the configuration file.
			var preferredBrowser = appSettings [ConfigKeys.PREFERRED_WEB_BROWSER];
			return (WebBrowser) Enum.Parse (typeof (WebBrowser), preferredBrowser);
		}

		public void SetCredentialsFor (ExecutionEnvironment executionEnv, IDictionary<UserRole, Credential> roleCredentialsDictionary)
		{
			this.envRolesAndCredentialsDictionary [executionEnv] = roleCredentialsDictionary;
		}
	}
}