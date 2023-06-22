using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Extensions;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace OOSelenium.Framework.Abstractions
{
	public abstract class MultiValuedControlBase
		: WebUiControlBase
	{
		protected readonly ReadOnlyCollection<IWebElement> entryTags;
		protected readonly IList<TextValuePair> entries;

		public MultiValuedControlBase (ReadOnlyCollection<IWebElement> entryTags, string id, IWebDriver webDriver)
			: base (entryTags ?[0], id, webDriver)
		{
			// The radio tags collection.
			this.entryTags = entryTags;

			// The radio options, their texts, and the corresponding values.
			this.entries = new List<TextValuePair> ();

			this.entries
				= new ReadOnlyCollection<TextValuePair> (this.entryTags
					.ToList ()
					.Select (et => new TextValuePair (et.GetInnerText (this.webDriver, this.id), et.GetAttribute ("value")))
					.ToList ());
		}

		protected IList<TextValuePair> GetSelectedEntries ()
		{
			return new ReadOnlyCollection<TextValuePair> (
				this.entryTags
					.Where (et => et.GetAttribute ("checked") == "true")
					.Select (et => new TextValuePair (et.GetInnerText (this.webDriver, this.id), et.GetAttribute ("value")))
					.ToList ());
		}

		protected void ClickAndSelectEntry (string entryText)
		{
			foreach (var oneTag in this.entryTags)
			{
				if (oneTag.GetInnerText (this.webDriver, this.id) == entryText)
				{
					oneTag.Click ();
					break;
				}
			}
		}
	}
}
