using System.Collections.ObjectModel;

using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class DropDownList
		: MultiValuedControlBase
	{
		public DropDownList (ReadOnlyCollection<IWebElement> dropDownEntryTags, string id, IWebDriver webDriver)
			: base (dropDownEntryTags, id, webDriver)
		{
		}

		public IList<TextValuePair> DropDownEntries
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