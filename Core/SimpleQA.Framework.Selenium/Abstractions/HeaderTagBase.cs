using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Extensions;

namespace SimpleQA.Framework.Selenium.Abstractions
{
	public abstract class HeaderTagBase
		: WebUiControlBase
	{
		public HeaderTagBase (string headerLevelIndicator, IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			if (element.TagName.ToLower () != headerLevelIndicator.ToLower ())
			{
				throw new ArgumentException ($"The provided element is not an <{headerLevelIndicator}> tag.", nameof (element));
			}
		}

		public override string? Text
		{
			get { return base.remoteElement.GetInnerText (base.webDriver); }
		}
	}
}