using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;

namespace SimpleQA.Framework.Selenium.WebUIControls
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