using Microsoft.Extensions.Configuration;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Misc;
using OOSelenium.WebUiTests.TestWebAppTests.Background;

namespace OOSelenium.WebUiTests.OtherTests.Background
{
	public sealed class CbaComAuDataProvider
		: IExecutionEnvironmentPageDataProvider<UserRole, ExecutionEnvironment>
	{
		private readonly IConfigurationRoot appSettings;

		public CbaComAuDataProvider ()
		{
			appSettings = new ConfigurationBuilder ().AddJsonFile ("appsettings.json").Build ();
		}

		public string GetWebApplicationUrlFor (ExecutionEnvironment testEnv)
		{
			return "https://www.cba.com.au";
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

		public void SetCredentialsFor (ExecutionEnvironment executionEnv, IDictionary<UserRole, Credential> roleCredentialsDictionary)
		{
			throw new NotImplementedException ();
		}
	}
}