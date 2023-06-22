using OOSelenium.Framework.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Button
		: WebUiControlBase
	{
		public Button (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
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