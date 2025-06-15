using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Extensions;
using OpenQA.Selenium;

namespace OOSelenium.Framework.Abstractions
{
	public abstract class WebUiControlBase
	{
		protected readonly string uniqueIdentifierText;
		protected readonly LocateByWhat uniqueIdentifierType;
		protected readonly IWebElement remoteElement;
		protected readonly IWebDriver webDriver;

		protected WebUiControlBase (IWebElement element, string identifierText, LocateByWhat byWhat, IWebDriver webDriver)
		{
			this.uniqueIdentifierType = byWhat;
			this.uniqueIdentifierText = identifierText ?? throw new ArgumentNullException (nameof (identifierText), "Id cannot be null when using ByWhat.Id.");

			this.remoteElement = element;
			this.webDriver = webDriver;
		}

		public IWebElement WebElement { get { return this.remoteElement; } }

		public string Id { get { return this.GetAttribute ("id"); } }

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