using System.Collections.Generic;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Extensions;

using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class ValidationSummary
		: WebUiControlBase
	{
		public ValidationSummary (RemoteWebElement element, string id)
			: base (element, id)
		{
		}

		public IList<string> ValidationFailureMessages
		{
			get { return this.remoteElement.ReadBulletEntries (this.webDriver, this.id); }
		}
	}
}