using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class TextField
		: WebUiControlBase
	{
		public TextField (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			if (element.TagName.ToLower () != "input")
			{
				throw new ArgumentException ("Element is not a text field", nameof (element));
			}
			if (element.GetAttribute ("type")?.ToLower () != "text" && element.GetAttribute ("type")?.ToLower () != "password")
			{
				throw new ArgumentException ("Element is not a text or password field", nameof (element));
			}
		}

		public bool IsPassword
		{
			get { return (base.GetAttribute ("type")?.ToLower ()?.Trim ()).Equals ("password");  }
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