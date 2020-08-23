using OOSelenium.Framework.Abstractions;

using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Label
		: WebUiControlBase
	{
		public Label (RemoteWebElement element, string id)
			: base (element, id)
		{
		}
	}
}