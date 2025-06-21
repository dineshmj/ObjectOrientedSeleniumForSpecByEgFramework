using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Extensions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class Button
		: WebUiControlBase
	{
		public Button (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			var tagName = element.TagName.ToLower ();

			if (tagName != "button" && tagName != "input")
			{
				throw new ArgumentException ("The provided element is not a <button> or <input> tag.", nameof (element));
			}
		}

		public override string? Text
		{
			get
			{
				var valueAttribute = this.GetAttribute ("value");

				if (valueAttribute.IsNotNullEmptyOrWhitespace ())
				{
					return valueAttribute;
				}

				var innerText = this.remoteElement.GetInnerText (base.webDriver);

				if (innerText.IsNotNullEmptyOrWhitespace ())
				{
					return innerText;
				}

				// If both value and inner text are null or empty, return a message.
				return "<button> or <input> element does not have any valid text. Please check your page's source.";
			}
		}
	}
}