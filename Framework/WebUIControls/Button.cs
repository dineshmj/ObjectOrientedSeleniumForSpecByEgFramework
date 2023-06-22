using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

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