using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;
using SimpleQA.Framework.Selenium.Extensions;

namespace SimpleQA.Framework.Selenium.WebUIControls
{
	public sealed class Link
		: WebUiControlBase
	{
		public Link (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			var tagName = element.TagName.ToLower ();

			if (tagName != "a" && tagName != "link")
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