using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Div
		: WebUiControlBase
	{
		public Div (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			if (element.TagName.ToLower () != "div")
			{
				throw new ArgumentException ("The provided element is not a <div> tag.", nameof (element));
			}
		}
	}
}