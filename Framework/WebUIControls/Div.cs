using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Div
		: WebUiControlBase
	{
		public Div (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
			if (element.TagName.ToLower () != "div")
			{
				throw new ArgumentException ("The provided element is not a <div> tag.", nameof (element));
			}
		}
	}
}