using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderFive
		: HeaderTagBase
	{
		public HeaderFive (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base ("h5", element, uniqueIdentifierText, byWhat, webDriver)
		{
		}
	}
}