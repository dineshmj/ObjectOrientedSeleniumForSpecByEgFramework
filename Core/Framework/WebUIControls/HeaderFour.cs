using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderFour
		: HeaderTagBase
	{
		public HeaderFour (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base ("h4", element, uniqueIdentifierText, byWhat, webDriver)
		{
		}
	}
}