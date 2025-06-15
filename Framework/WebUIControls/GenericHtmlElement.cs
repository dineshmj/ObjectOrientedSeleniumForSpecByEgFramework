using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class GenericHtmlElement
		: WebUiControlBase
	{
		public GenericHtmlElement (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
		}

		public override string? Text
		{
			get { return base.remoteElement.GetAttribute ("value"); }
		}
	}
}