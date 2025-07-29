using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;
using SimpleQA.Framework.Selenium.Extensions;

namespace SimpleQA.Framework.Selenium.WebUIControls
{
	public sealed class ValidationLabel
		: WebUiControlBase
	{
		public ValidationLabel (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			var tagName = element.TagName.ToLower ();

			if (tagName != "span" && tagName != "label")
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