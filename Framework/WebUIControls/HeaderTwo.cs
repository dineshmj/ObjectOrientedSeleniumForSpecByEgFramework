using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderTwo
		: HeaderTagBase
	{
		public HeaderTwo (IWebElement element, string id, IWebDriver webDriver)
			: base ("h2", element, id, webDriver)
		{
		}
	}
}