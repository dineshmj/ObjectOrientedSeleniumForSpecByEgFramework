using Microsoft.Extensions.Configuration;

using SimpleQA.Framework.Abstractions;
using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Misc;
using SimpleQA.UseCases.Selenium.WebUiTests.Entities;
using Environment = SimpleQA.UseCases.Selenium.WebUiTests.Entities.Environment;

namespace SimpleQA.UseCases.Selenium.WebUITests.TestWebAppTests.DataProviders
{
	public sealed class InsuranceOneTestDataProvider
		: IEnvironmentDataProvider<UserRole, Environment>
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
		private IDictionary<Environment, IDictionary<UserRole, Credential>> envRolesAndCredentialsDictionary;

		public InsuranceOneTestDataProvider ()
        {
			appSettings
				= new ConfigurationBuilder ()
					.AddJsonFile ("appsettings.json")
					.Build ();

			this.envRolesAndCredentialsDictionary
				= new Dictionary<Environment, IDictionary<UserRole, Credential>>
					{
						{ Environment.FunctionalTest, this.functionalTestEnvRolesAndCredentialsDictionary }
					};
		}

		public string GetApplicationUrlFor (Environment executionEnv)
		{
			return
				executionEnv switch
					{
						Environment.FunctionalTest => "http://localhost:50004/",
						_ => throw new NotImplementedException ($"Execution Environment, \" {executionEnv} \" is not yet implemented."),
					};
		}

		public Environment GetExecutionEnvironment ()
		{
			// Modify the logic such that it is taken from the configuration file.
			return Environment.FunctionalTest;
		}

		public IDictionary<UserRole, Credential> GetCredentialsFor (Environment testEnv)
		{
			return
				testEnv switch
					{
						Environment.FunctionalTest => this.envRolesAndCredentialsDictionary [testEnv],
						_ => throw new NotImplementedException ($"Test Environment, \" {testEnv} \" is not yet implemented."),
					};
		}

		public WebBrowser GetPreferredWebBrowser ()
		{
			// Modify the logic such that it is taken from the configuration file.
			var preferredBrowser = appSettings [ConfigKeys.PREFERRED_WEB_BROWSER];
			return (WebBrowser) Enum.Parse (typeof (WebBrowser), preferredBrowser);
		}

		public void SetCredentialsFor (Environment executionEnv, IDictionary<UserRole, Credential> roleCredentialsDictionary)
		{
			this.envRolesAndCredentialsDictionary [executionEnv] = roleCredentialsDictionary;
		}
	}
}