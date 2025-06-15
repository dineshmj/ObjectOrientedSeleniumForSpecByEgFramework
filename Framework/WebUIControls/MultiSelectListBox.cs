using System.Collections.ObjectModel;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class MultiSelectListBox
		: MultiValuedControlBase
	{
		private readonly SelectElement parentSelectTag;

		public MultiSelectListBox (ReadOnlyCollection<IWebElement> multiListBoxEntryTags, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (multiListBoxEntryTags, uniqueIdentifierText, byWhat, webDriver)
		{
			if (multiListBoxEntryTags == null || multiListBoxEntryTags.Count == 0)
			{
				throw new ArgumentException ("The provided collection of multi-select list box entries is empty.", nameof (multiListBoxEntryTags));
			}

			// Get the parent "select" tag.
			var parentTag = base.entryTags [0];

			while (true)
			{
				// Go one level up.
				parentTag = parentTag.FindElement (By.XPath (".."));

				if (parentTag == null)
				{
					break;
				}

				// Is the parent tag a "select" tag?
				if (parentTag.TagName == "select")
				{
					this.parentSelectTag = new SelectElement (parentTag);
					break;
				}
			}
		}

		public IList<TextValuePair> ListEntries
		{
			get  { return base.entries; }
		}

		public IList<TextValuePair> SelectedEntries
		{
			get { return base.GetSelectedEntries (); }
		}

		public void SelectTheseValues (params string [] multiListBoxTextsToBeSet)
		{
			// Walk through the list, and compare.
			foreach (var oneValue in multiListBoxTextsToBeSet)
			{
				for (var index = 0; index < this.entries.Count; index++)
				{
					// Does the value match with entry?
					if (this.entries [index].Text == oneValue)
					{
						// Select it.
						this.parentSelectTag.SelectByIndex (index);
						break;
					}
				}
			}
		}
	}
}