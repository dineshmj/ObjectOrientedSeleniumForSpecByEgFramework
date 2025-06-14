using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderFive
		: HeaderTagBase
	{
		public HeaderFive (IWebElement element, string id, IWebDriver webDriver)
			: base ("h5", element, id, webDriver)
		{
		}
	}
}