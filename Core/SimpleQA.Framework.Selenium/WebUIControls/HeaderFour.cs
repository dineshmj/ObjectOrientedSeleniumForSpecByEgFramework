using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;

namespace SimpleQA.Framework.Selenium.WebUIControls
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