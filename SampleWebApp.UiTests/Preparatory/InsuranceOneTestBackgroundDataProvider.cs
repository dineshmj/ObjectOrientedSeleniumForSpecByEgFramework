﻿using System;
using System.Collections.Generic;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

using SampleWebApp.UiTests.Entities;

namespace SampleWebApp.UiTests.Preparatory
{
	public sealed class InsuranceOneTestBackgroundDataProvider
		: ITestBackgroundDataProvider<UserRole, TestEnvironment>
	{
		public string GetTargetApplicationBaseUrlFor (TestEnvironment testEnv)
		{
			switch (testEnv)
			{
				case TestEnvironment.FunctionalTest:
					return "https://localhost:44399/";

				default:
					throw new NotImplementedException ($"Test Environment, \" { testEnv } \" is not yet implemented.");
			}
		}

		public TestEnvironment GetTargetTestEnvironment ()
		{
			return TestEnvironment.FunctionalTest;
		}

		public IDictionary<UserRole, Credential> GetValidCredentialsOfUserTypesFor (TestEnvironment testEnv)
		{
			switch (testEnv)
			{
				case TestEnvironment.FunctionalTest:
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

		public WebBrowser GetWebBrowserTypeToUseForAcceptanceTests ()
		{
			return WebBrowser.GoogleChrome;
		}
	}
}