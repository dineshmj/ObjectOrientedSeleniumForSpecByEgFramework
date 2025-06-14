using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderOne
		: HeaderTagBase
	{
		public HeaderOne (IWebElement element, string id, IWebDriver webDriver)
			: base ("h1", element, id, webDriver)
		{
		}
	}
}