using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Span
		: WebUiControlBase
	{
		public Span (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
			if (element.TagName.ToLower () != "span")
			{
				throw new ArgumentException ("Element is not a span", nameof (element));
			}
		}
	}
}