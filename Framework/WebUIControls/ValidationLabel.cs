using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class ValidationLabel
		: WebUiControlBase
	{
		public ValidationLabel (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
		}

		public override string Text
		{
			get { return base.remoteElement.GetInnerText (base.webDriver, base.id); }
		}
	}
}