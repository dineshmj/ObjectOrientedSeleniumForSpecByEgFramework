using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class HeaderThree
		: HeaderTagBase
	{
		public HeaderThree (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base ("h3", element, uniqueIdentifierText, byWhat, webDriver)
		{
		}
	}
}