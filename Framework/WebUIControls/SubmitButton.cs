using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class SubmitButton
		: WebUiControlBase
	{
		public SubmitButton (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			if (element.TagName.ToLower () != "input")
			{
				throw new ArgumentException ("The provided element is not a <input> tag.", nameof (element));
			}
		}

		public override string? Text
		{
			get { return base.remoteElement.GetAttribute ("value"); }
		}
	}
}