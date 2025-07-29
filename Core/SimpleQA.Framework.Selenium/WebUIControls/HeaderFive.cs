using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;

namespace SimpleQA.Framework.Selenium.WebUIControls
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