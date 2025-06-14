using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderThree
		: HeaderTagBase
	{
		public HeaderThree (IWebElement element, string id, IWebDriver webDriver)
			: base ("h3", element, id, webDriver)
		{
		}
	}
}