using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class CheckBox
		: WebUiControlBase
	{
		public CheckBox (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			var tagName = element.TagName.ToLower ();

			if (tagName != "input" || element.GetAttribute ("type").ToLower () != "checkbox")
			{
				throw new ArgumentException ("The provided element is not a <input type='checkbox'> tag.", nameof (element));
			}
		}

		public bool IsChecked
		{
			get { return base.remoteElement.GetAttribute ("checked") == "true"; }
		}

		public override string Text
		{
			get { return base.remoteElement.FindElement (By.XPath ("..")).Text;  }
		}

		public string Value
		{
			get { return base.remoteElement.GetAttribute ("value"); }
		}

		public void ToggleCheckState ()
		{
			base.remoteElement.Click ();
		}
	}
}