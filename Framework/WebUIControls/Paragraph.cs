using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Paragraph
		: WebUiControlBase
	{
		public Paragraph (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
			if (element.TagName.ToLower () != "p")
			{
				throw new ArgumentException ("Element is not a paragraph", nameof (element));
			}
		}
	}
}