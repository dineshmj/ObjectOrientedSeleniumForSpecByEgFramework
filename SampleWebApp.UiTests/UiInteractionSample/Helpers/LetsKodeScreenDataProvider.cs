using System;
using System.Collections.Generic;
using System.Configuration;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Misc;

using SampleWebApp.UiTests.Entities;

namespace SampleWebApp.UiTests.UiInteractionSample.Helpers
{
	public sealed class LetsKodeScreenDataProvider
		: ITestBackgroundDataProvider<UserRole, TestEnvironment>
	{
		public string GetTargetApplicationBaseUrlFor (TestEnvironment testEnv)
		{
			return "https://letskodeit.teachable.com/p/practice";
		}

		public TestEnvironment GetTargetTestEnvironment ()
		{
			return TestEnvironment.FunctionalTest;
		}

		public IDictionary<UserRole, Credential> GetValidCredentialsOfUserTypesFor (TestEnvironment testEnv)
		{
			return new Dictionary<UserRole, Credential> ();
		}

		public WebBrowser GetWebBrowserTypeToUseForAcceptanceTests ()
		{
			var preferredBrowser = ConfigurationManager.AppSettings [Constants.PREFERRED_WEB_BROWSER_KEY];
			return (WebBrowser) Enum.Parse (typeof (WebBrowser), preferredBrowser);
		}
	}
}
