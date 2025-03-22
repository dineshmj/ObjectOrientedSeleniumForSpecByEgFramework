using Microsoft.Extensions.Configuration;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Misc;
using SampleWebApp.UiTests.LoginTests.Background;

namespace SampleWebApp.UiTests.LoginTests.DataProviders
{
	public sealed class InsuranceOneTestDataProvider
		: IExecutionEnvironmentDataProvider<UserRole, ExecutionEnvironment>
	{
		private readonly IConfigurationRoot appSettings;

		public InsuranceOneTestDataProvider()
        {
			appSettings
				= new ConfigurationBuilder ()
					.AddJsonFile ("appsettings.json")
					.Build ();
		}

		public string GetApplicationBaseUrlFor (ExecutionEnvironment executionEnv)
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
			return ExecutionEnvironment.FunctionalTest;
		}

		public IDictionary<UserRole, Credential> GetCredentialsFor (ExecutionEnvironment testEnv)
		{
			switch (testEnv)
			{
				case ExecutionEnvironment.FunctionalTest:
					return new Dictionary<UserRole, Credential>
					{
						{ UserRole.Admin, new Credential (userId: "admin", encryptedPassword: "123") },
						{ UserRole.QuoteIssuer, new Credential (userId: "quote_issuer1", encryptedPassword: "123")  },
						{ UserRole.ProposalInitiator, new Credential (userId: "proposer1", encryptedPassword: "123")  },
						{ UserRole.PolicyApprover, new Credential (userId: "policy_approver1", encryptedPassword: "123")  },
					};

				default:
					throw new NotImplementedException ($"Test Environment, \" { testEnv } \" is not yet implemented.");
			}
		}

		public WebBrowser GetPreferredWebBrowser ()
		{
			var preferredBrowser = appSettings [ConfigKeys.PREFERRED_WEB_BROWSER];
			return (WebBrowser) Enum.Parse (typeof (WebBrowser), preferredBrowser);
		}
	}
}