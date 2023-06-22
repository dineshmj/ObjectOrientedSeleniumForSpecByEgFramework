using OOSelenium.Framework.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Label
		: WebUiControlBase
	{
		public Label (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
		}
	}
}