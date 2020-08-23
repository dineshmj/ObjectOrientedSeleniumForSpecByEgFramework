using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.Abstractions
{
	public abstract class WebUiControlBase
	{
		protected readonly string id;
		protected readonly RemoteWebElement remoteElement;
		protected readonly IWebDriver webDriver;

		protected WebUiControlBase (RemoteWebElement element, string id)
		{
			this.id = id;
			this.remoteElement = element;
			this.webDriver = element.WrappedDriver;
		}

		public string Id { get { return this.id; } }

		public virtual string Text 
		{
			get { return this.remoteElement.Text;  }
		}
	}
}