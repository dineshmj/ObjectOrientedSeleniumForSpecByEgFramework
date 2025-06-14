﻿using System.Collections.ObjectModel;

using OpenQA.Selenium;

using OOSelenium.Framework.Entities;
using OOSelenium.Framework.Extensions;

namespace OOSelenium.Framework.Abstractions
{
	public abstract class MultiValuedControlBase
		: WebUiControlBase
	{
		protected readonly ReadOnlyCollection<IWebElement> entryTags;
		protected readonly IList<TextValuePair> entries;

		public MultiValuedControlBase (ReadOnlyCollection<IWebElement> entryTags, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (entryTags ?[0], uniqueIdentifierText, byWhat, webDriver)
		{
			// The radio tags collection.
			this.entryTags = entryTags;

			// The radio options, their texts, and the corresponding values.
			this.entries
				= new ReadOnlyCollection<TextValuePair> (this.entryTags
					.ToList ()
					.Select (et => new TextValuePair (et.GetInnerText (this.webDriver, this.uniqueIdentifierText), et.GetAttribute ("value")))
					.ToList ());
		}

		protected IList<TextValuePair> GetSelectedEntries ()
		{
			return new ReadOnlyCollection<TextValuePair> (
				this.entryTags
					.Where (et => et.GetAttribute ("checked") == "true")
					.Select (et => new TextValuePair (et.GetInnerText (this.webDriver, this.uniqueIdentifierText), et.GetAttribute ("value")))
					.ToList ());
		}

		protected void ClickAndSelectEntry (string entryText)
		{
			foreach (var oneTag in this.entryTags)
			{
				if (oneTag.GetInnerText (this.webDriver, this.uniqueIdentifierText) == entryText)
				{
					oneTag.Click ();
					break;
				}
			}
		}
	}
}