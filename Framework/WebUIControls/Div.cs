using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Div
		: WebUiControlBase
	{
		public Div (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
		}
	}
}