using OOSelenium.Framework.Abstractions;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Button
		: WebUiControlBase
	{
		public Button (RemoteWebElement element, string id)
			: base (element, id)
		{
		}

		public void Click ()
		{
			base.remoteElement.Click ();
		}

		public override string Text
		{
			get { return base.remoteElement.GetAttribute ("value"); }
		}
	}
}