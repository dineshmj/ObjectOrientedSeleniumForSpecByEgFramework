using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderSix
		: HeaderTagBase
	{
		public HeaderSix (IWebElement element, string id, IWebDriver webDriver)
			: base ("h6", element, id, webDriver)
		{
		}
	}
}