using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

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
			return WebBrowser.GoogleChrome;
		}
	}
}
