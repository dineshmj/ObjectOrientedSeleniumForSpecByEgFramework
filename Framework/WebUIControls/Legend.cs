using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Legend
		: WebUiControlBase
	{
		public Legend (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
			if (element.TagName.ToLower () != "legend")
			{
				throw new ArgumentException ("Element is not a legend", nameof (element));
			}
		}
	}
}