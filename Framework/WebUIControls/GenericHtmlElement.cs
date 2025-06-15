using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class GenericHtmlElement
		: WebUiControlBase
	{
		public GenericHtmlElement (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
		}

		public override string? Text
		{
			get { return base.remoteElement.GetAttribute ("value"); }
		}
	}
}