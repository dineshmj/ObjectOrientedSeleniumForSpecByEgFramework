using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class ValidationLabel
		: WebUiControlBase
	{
		public ValidationLabel (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			if (element.TagName.ToLower () != "span" && element.TagName.ToLower () != "label")
			{
				throw new ArgumentException ("Element is not a span or label", nameof (element));
			}

			if (!element.GetAttribute ("class").Contains ("validation"))
			{
				throw new ArgumentException ("Element does not have a validation class", nameof (element));
			}
		}

		public override string Text
		{
			get { return base.remoteElement.GetInnerText (base.webDriver, base.uniqueIdentifierText); }
		}
	}
}