using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Link
		: WebUiControlBase
	{
		public Link (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			if (element.TagName.ToLower () != "a")
			{
				throw new ArgumentException ("Element is not a link", nameof (element));
			}
		}

		public override string Text
		{
			get { return base.remoteElement.GetInnerText (base.webDriver, base.uniqueIdentifierText); }
		}
	}
}