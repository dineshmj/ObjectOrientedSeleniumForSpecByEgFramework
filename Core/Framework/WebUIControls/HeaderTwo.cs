using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderTwo
		: HeaderTagBase
	{
		public HeaderTwo (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base ("h2", element, uniqueIdentifierText, byWhat, webDriver)
		{
		}
	}
}