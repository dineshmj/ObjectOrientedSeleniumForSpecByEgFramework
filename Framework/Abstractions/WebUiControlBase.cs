using OOSelenium.Framework.Extensions;
using OpenQA.Selenium;

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

		public IWebElement WebElement { get { return this.remoteElement; } }

		public string Id { get { return this.id; } }

		public virtual string? Text 
		{
			get { return this.remoteElement.Text;  }
		}

		public virtual string InnerText
		{
			get { return this.remoteElement.GetInnerText (this.webDriver); }
		}

		public virtual string CssClass
		{
			get { return this.GetAttribute ("class");  }
		}

		public virtual void SetFocus ()
		{
			this.remoteElement.SetFocus (this.webDriver);
		}
		public void Click ()
		{
			this.remoteElement.Click ();
		}

		// Protected methods.

		protected string GetAttribute (string attributeName)
		{
			return this.remoteElement.GetAttribute (attributeName);
		}
	}
}