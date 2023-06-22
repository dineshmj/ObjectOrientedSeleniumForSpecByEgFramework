using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.Abstractions
{
	public abstract class WebUiControlBase
	{
		protected readonly string id;
		protected readonly IWebElement remoteElement;
		protected readonly IWebDriver webDriver;

		protected WebUiControlBase (IWebElement element, string id, IWebDriver webDriver)
		{
			this.id = id;
			this.remoteElement = element;
			this.webDriver = webDriver;
		}

		public string Id { get { return this.id; } }

		public virtual string Text 
		{
			get { return this.remoteElement.Text;  }
		}

		public virtual string CssClass
		{
			get { return this.GetAttribute ("class");  }
		}

		// Protected methods.

		protected string GetAttribute (string attributeName)
		{
			return this.remoteElement.GetAttribute (attributeName);
		}
	}
}