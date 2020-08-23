using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;

using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Link
		: WebUiControlBase
	{
		public Link (RemoteWebElement element, string id)
			: base (element, id)
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