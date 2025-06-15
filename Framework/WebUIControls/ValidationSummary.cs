using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class ValidationSummary
		: WebUiControlBase
	{
		public ValidationSummary (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			var tagName = element.TagName.ToLower ();

			if (tagName != "div")
			{
				throw new ArgumentException ("Element is not a <div> tag", nameof (element));
			}

			if (!element.GetAttribute ("class").Contains ("validation"))
			{
				throw new ArgumentException ("Element does not have a validation class", nameof (element));
			}
		}

		public IList<string> ValidationFailureMessages
		{
			get { return base.remoteElement.ReadBulletEntries (base.webDriver, base.uniqueIdentifierText); }
		}
	}
}