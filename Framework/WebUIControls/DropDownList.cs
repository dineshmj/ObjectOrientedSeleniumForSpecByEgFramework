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
			if (dropDownEntryTags == null || dropDownEntryTags.Count == 0)
			{
				throw new ArgumentException ("The provided collection of drop-down entries is empty.", nameof (dropDownEntryTags));
			}
			if (dropDownEntryTags [0].TagName.ToLower () != "select")
			{
				throw new ArgumentException ("The provided element is not a <select> tag.", nameof (dropDownEntryTags));
			}
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