using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Link
		: WebUiControlBase
	{
		public Link (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
		}

		public void Click ()
		{
			base.remoteElement.Click ();
		}

		public override string Text
		{
			get { return base.remoteElement.GetInnerText (base.webDriver, base.id); }
		}
	}
}