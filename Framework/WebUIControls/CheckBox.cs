using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class CheckBox
		: WebUiControlBase
	{
		public CheckBox (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
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