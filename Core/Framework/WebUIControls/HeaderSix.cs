using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderSix
		: HeaderTagBase
	{
		public HeaderSix (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base ("h6", element, uniqueIdentifierText, byWhat, webDriver)
		{
		}
	}
}