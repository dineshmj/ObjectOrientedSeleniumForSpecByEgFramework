using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderOne
		: HeaderTagBase
	{
		public HeaderOne (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base ("h1", element, uniqueIdentifierText, byWhat, webDriver)
		{
		}
	}
}