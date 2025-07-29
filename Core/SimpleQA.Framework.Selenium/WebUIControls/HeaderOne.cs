using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;

namespace SimpleQA.Framework.Selenium.WebUIControls
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