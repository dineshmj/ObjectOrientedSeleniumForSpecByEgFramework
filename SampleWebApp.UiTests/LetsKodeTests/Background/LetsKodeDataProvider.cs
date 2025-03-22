using Microsoft.Extensions.Configuration;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Misc;
using SampleWebApp.UiTests.LoginTests.Background;

namespace SampleWebApp.UiTests.LetsKodeTests.Background
{
	public sealed class LetsKodeDataProvider
		: IExecutionEnvironmentDataProvider<UserRole, ExecutionEnvironment>
	{
		private readonly IConfigurationRoot appSettings;
		
		public LetsKodeDataProvider ()
        {
			appSettings = new ConfigurationBuilder ().AddJsonFile ("appsettings.json").Build ();
		}

		public string GetApplicationBaseUrlFor (ExecutionEnvironment testEnv)
		{
			return "https://www.letskodeit.com/practice";
		}

		public ExecutionEnvironment GetExecutionEnvironment ()
		{
			return ExecutionEnvironment.FunctionalTest;
		}

		public IDictionary<UserRole, Credential> GetCredentialsFor (ExecutionEnvironment testEnv)
		{
			return new Dictionary<UserRole, Credential> ();
		}

		public WebBrowser GetPreferredWebBrowser ()
		{
			var preferredBrowser = appSettings [ConfigKeys.PREFERRED_WEB_BROWSER];
			return (WebBrowser) Enum.Parse (typeof (WebBrowser), preferredBrowser);
		}
	}
}