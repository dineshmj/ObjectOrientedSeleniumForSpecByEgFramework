using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderFour
		: HeaderTagBase
	{
		public HeaderFour (IWebElement element, string id, IWebDriver webDriver)
			: base ("h4", element, id, webDriver)
		{
		}
	}
}