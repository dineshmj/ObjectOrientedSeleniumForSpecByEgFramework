using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class TextField
		: WebUiControlBase
	{
		public TextField (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
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