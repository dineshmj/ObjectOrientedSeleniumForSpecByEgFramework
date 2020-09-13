using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;

using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class ValidationLabel
		: WebUiControlBase
	{
		public ValidationLabel (RemoteWebElement element, string id)
			: base (element, id)
		{
		}

		public override string Text
		{
			get { return base.remoteElement.GetInnerText (base.webDriver, base.id); }
		}
	}
}