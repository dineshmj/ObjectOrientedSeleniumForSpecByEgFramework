using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Span
		: WebUiControlBase
	{
		public Span (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			if (element.TagName.ToLower () != "span")
			{
				throw new ArgumentException ("Element is not a span", nameof (element));
			}
		}
	}
}