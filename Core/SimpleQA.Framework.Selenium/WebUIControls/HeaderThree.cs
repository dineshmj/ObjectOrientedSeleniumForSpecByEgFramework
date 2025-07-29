using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;

namespace SimpleQA.Framework.Selenium.WebUIControls
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