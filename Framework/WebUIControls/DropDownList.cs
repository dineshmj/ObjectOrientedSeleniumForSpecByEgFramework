using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using OOSelenium.Framework.Entities;

using OpenQA.Selenium;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class DropDownList
		: MultiValuedControlBase
	{
		public DropDownList (ReadOnlyCollection<IWebElement> dropDownEntryTags, string id)
			: base (dropDownEntryTags, id)
		{
		}

		public IList<TextValuePair> DropDownOptions
		{
			get  { return base.entries; }
		}

		public TextValuePair SelectedEntry
		{
			get { return base.GetSelectedEntries ().FirstOrDefault (); }
		}

		public void SetSelectionTo (string dropDownTextToBeSet)
		{
			base.ClickAndSelectEntry (dropDownTextToBeSet);
		}
	}
}
