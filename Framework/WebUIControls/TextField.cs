using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class TextField
		: WebUiControlBase
	{
		public TextField (RemoteWebElement element, string id)
			: base (element, id)
		{
		}

		public void SetText (string text)
		{
			base.remoteElement.SetValue (text, base.webDriver);
		}
	}
}