using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Abstractions;
using SimpleQA.Framework.Selenium.Extensions;

namespace SimpleQA.Framework.Selenium.WebUIControls
{
	public sealed class TextField
		: WebUiControlBase
	{
		public TextField (IWebElement element, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (element, uniqueIdentifierText, byWhat, webDriver)
		{
			var tagName = element.TagName.ToLower ();

			if (tagName != "input")
			{
				throw new ArgumentException ("Element is not a text field", nameof (element));
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
			// The remoteElement.SetValue () method does not trigger the 'input' event in some Pega-rendered web applications.
			// This is a workaround to ensure that the 'input' event is triggered, which is necessary for some applications to recognize the change.

			var js = (IJavaScriptExecutor) base.webDriver;
			js.ExecuteScript ("arguments[0].value = arguments[1]; arguments[0].dispatchEvent(new Event('input', { bubbles : true }));", base.remoteElement, text);
		}

		public void Clear ()
		{
			base.remoteElement.Clear ();
		}

		public void SetFocus ()
		{
			base.remoteElement.SetFocus (base.webDriver);
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