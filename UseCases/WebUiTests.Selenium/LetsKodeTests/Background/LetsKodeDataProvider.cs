using Microsoft.Extensions.Configuration;

using SimpleQA.Framework.Abstractions;
using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Misc;
using SimpleQA.UseCases.Selenium.WebUiTests.Entities;

namespace SimpleQA.UseCases.Selenium.WebUITests.LetsKodeTests.Background
{
	public sealed class LetsKodeDataProvider
		: IEnvironmentDataProvider<UserRole, WebUiTests.Entities.Environment>
	{
		private readonly IConfigurationRoot appSettings;
		
		public LetsKodeDataProvider ()
        {
			appSettings = new ConfigurationBuilder ().AddJsonFile ("appsettings.json").Build ();
		}

		public string GetApplicationUrlFor (WebUiTests.Entities.Environment testEnv)
		{
			return "https://www.letskodeit.com/practice";
		}

		public WebUiTests.Entities.Environment GetExecutionEnvironment ()
		{
			return WebUiTests.Entities.Environment.FunctionalTest;
		}

		public IDictionary<UserRole, Credential> GetCredentialsFor (WebUiTests.Entities.Environment testEnv)
		{
			return new Dictionary<UserRole, Credential> ();
		}

		public WebBrowser GetPreferredWebBrowser ()
		{
			var preferredBrowser = appSettings [ConfigKeys.PREFERRED_WEB_BROWSER];
			return (WebBrowser) Enum.Parse (typeof (WebBrowser), preferredBrowser);
		}

		public void SetCredentialsFor (WebUiTests.Entities.Environment executionEnv, IDictionary<UserRole, Credential> roleCredentialsDictionary)
		{
			throw new NotImplementedException ();
		}
	}
}