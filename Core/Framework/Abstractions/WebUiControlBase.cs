using OpenQA.Selenium;

using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Extensions;

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
			this.uniqueIdentifierText = identifierText ?? throw new ArgumentNullException (nameof (identifierText), "Unique identifier cannot be null.");

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
			if (this.remoteElement == null)
			{
				return;
			}

			var js = (IJavaScriptExecutor) this.webDriver;

			var tagName = this.remoteElement.TagName.ToLowerInvariant ();
			var tagType = this.remoteElement.GetAttribute ("type") ?? string.Empty;

			//
			// Precaution for web applications that are Pega-rendered - there can be DIV and other elements
			// that will be overlaying the actual input element, so we need to ensure that we are clicking.
			//
			if (tagName == "input" && tagType == "radio")
			{
				// Pega expects Events triggered as part of user activities. So, JS has to simulate that behavior.
				js.ExecuteScript ("arguments[0].focus();", this.remoteElement);
				Thread.Sleep (100);

				js.ExecuteScript ("arguments[0].dispatchEvent(new MouseEvent('mousedown', {bubbles : true}));", this.remoteElement);
				Thread.Sleep (100);

				js.ExecuteScript ("arguments[0].dispatchEvent(new MouseEvent('mouseup', {bubbles : true}));", this.remoteElement);
				Thread.Sleep (100);

				js.ExecuteScript ("arguments[0].click();", this.remoteElement);
				Thread.Sleep (100);

				js.ExecuteScript ("arguments[0].dispatchEvent(new MouseEvent('click', {bubbles : true}));", this.remoteElement);
				Thread.Sleep (100);

				js.ExecuteScript ("arguments[0].dispatchEvent(new MouseEvent('change', {bubbles : true}));", this.remoteElement);
				Thread.Sleep (100);
			}
			else if (tagName == "input" && tagType == "checkbox")
			{
				js.ExecuteScript ("arguments[0].checked = !arguments[0].checked; arguments[0].dispatchEvent(new Event('change', {bubbles : true}));", this.remoteElement);
				Thread.Sleep (100);
			}
			else if (tagName == "button" || tagName == "a" || tagName == "input")
			{
				// Pega expects Events triggered as part of user activities. So, JS has to simulate that behavior.
				js.ExecuteScript ("arguments[0].focus();", this.remoteElement);
				Thread.Sleep (100);
				js.ExecuteScript ("arguments[0].dispatchEvent(new MouseEvent('mousedown', {bubbles : true}));", this.remoteElement);
				Thread.Sleep (100);
				js.ExecuteScript ("arguments[0].dispatchEvent(new MouseEvent('mouseup', {bubbles : true}));", this.remoteElement);
				Thread.Sleep (100);
				js.ExecuteScript ("arguments[0].click();", this.remoteElement);
				Thread.Sleep (100);
				js.ExecuteScript ("arguments[0].dispatchEvent(new MouseEvent('click', {bubbles : true}));", this.remoteElement);
				Thread.Sleep (100);
			}
			else
			{
				try
				{
					this.remoteElement.Click ();
				}
				catch
				{
				}
			}
		}

		// Protected methods.

		protected string GetAttribute (string attributeName)
		{
			return this.remoteElement.GetAttribute (attributeName);
		}
	}
}