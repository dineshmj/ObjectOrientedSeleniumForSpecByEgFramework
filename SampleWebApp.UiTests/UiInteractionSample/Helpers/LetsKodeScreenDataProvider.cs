using Microsoft.Extensions.Configuration;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Misc;

using SampleWebApp.UiTests.Entities;

namespace SampleWebApp.UiTests.UiInteractionSample.Helpers
{
	public sealed class LetsKodeScreenDataProvider
		: ITestBackgroundDataProvider<UserRole, TestEnvironment>
	{
		private readonly IConfigurationRoot appSettings;
		
		public LetsKodeScreenDataProvider ()
        {
			this.appSettings = new ConfigurationBuilder ().AddJsonFile ("appsettings.json").Build ();
		}

		public string GetTargetApplicationBaseUrlFor (TestEnvironment testEnv)
		{
			return "https://www.letskodeit.com/practice";
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
			var preferredBrowser = this.appSettings [ConfigKeys.PREFERRED_WEB_BROWSER];
			return (WebBrowser) Enum.Parse (typeof (WebBrowser), preferredBrowser);
		}
	}
}