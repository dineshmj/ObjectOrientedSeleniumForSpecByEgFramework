using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class ValidationSummary
		: WebUiControlBase
	{
		public ValidationSummary (IWebElement element, string id, IWebDriver webDriver)
			: base (element, id, webDriver)
		{
			if (element.TagName.ToLower () != "ul")
			{
				throw new ArgumentException ("Element is not a <ul> tag", nameof (element));
			}
			if (!element.GetAttribute ("class").Contains ("validation"))
			{
				throw new ArgumentException ("Element does not have a validation class", nameof (element));
			}
		}

		public IList<string> ValidationFailureMessages
		{
			get { return base.remoteElement.ReadBulletEntries (base.webDriver, base.id); }
		}
	}
}