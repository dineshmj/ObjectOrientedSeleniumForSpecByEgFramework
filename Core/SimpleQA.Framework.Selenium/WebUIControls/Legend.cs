using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;

namespace SimpleQA.Framework.Selenium.WebUIControls
{
	public sealed class Legend
		: WebUiControlBase
	{
		public Legend (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			var tagName = element.TagName.ToLower ();

			if (tagName != "legend")
			{
				throw new ArgumentException ("Element is not a legend", nameof (element));
			}
		}
	}
}