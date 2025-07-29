using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;

namespace SimpleQA.Framework.Selenium.WebUIControls
{
	public sealed class Label
		: WebUiControlBase
	{
		public Label (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			var tagName = element.TagName.ToLower ();

			if (tagName != "label")
			{
				throw new ArgumentException ("Element is not a label", nameof (element));
			}
		}
	}
}