using OOSelenium.Framework.Abstractions;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class CheckBox
		: WebUiControlBase
	{
		public CheckBox (RemoteWebElement element, string id)
			: base (element, id)
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