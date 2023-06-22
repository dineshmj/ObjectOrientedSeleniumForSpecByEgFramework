using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Label
		: WebUiControlBase
	{
		public Label (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
		}
	}
}