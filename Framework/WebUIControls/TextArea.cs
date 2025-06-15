using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class TextArea
		: WebUiControlBase
	{
		public TextArea (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			if (element.TagName.ToLowerInvariant () != "textarea")
			{
				throw new ArgumentException ("The provided element is not a textarea.", nameof (element));
			}
		}

		public string PlaceHolderText
		{
			get { return base.GetAttribute ("placeholder"); }
		}

		public void SetText (string text)
		{
			base.remoteElement.SetValue (text, base.webDriver);
		}

		public void Clear ()
		{
			base.remoteElement.Clear ();
		}

		public void SendKeys (string keys)
		{
			base.remoteElement.SendKeys (keys);
		}

		public void TypeEachCharacter (string text)
		{
			foreach (char oneChar in text)
			{
				base.remoteElement.SendKeys (oneChar.ToString ());
			}
		}
	}
}