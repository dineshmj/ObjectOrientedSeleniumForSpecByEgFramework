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
		}

		public IList<string> ValidationFailureMessages
		{
			get { return base.remoteElement.ReadBulletEntries (base.webDriver, base.id); }
		}
	}
}