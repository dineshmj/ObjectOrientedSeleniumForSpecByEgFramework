using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

using OpenQA.Selenium;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class RadioButtons
		: MultiValuedControlBase
	{
		public RadioButtons (ReadOnlyCollection<IWebElement> radioTags, string id, IWebDriver webDriver)
			: base (radioTags, id, webDriver)
		{
		}

		public IList<TextValuePair> RadioOptions
		{
			get  { return base.entries; }
		}

		public TextValuePair SelectedOption
		{
			get { return base.GetSelectedEntries ().FirstOrDefault (); }
		}

		public void SetRadioTo (string selectedRadioText)
		{
			base.ClickAndSelectEntry (selectedRadioText);
		}
	}
}
