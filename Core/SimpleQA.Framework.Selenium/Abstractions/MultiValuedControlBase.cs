using System.Collections.ObjectModel;

using OpenQA.Selenium;

using SimpleQA.Framework.Entities;
using SimpleQA.Framework.Selenium.Extensions;

namespace SimpleQA.Framework.Selenium.Abstractions
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
			var index = 0;

			foreach (var oneTag in this.entryTags)
			{
				var oneEntryText = oneTag.GetInnerText (this.webDriver, this.uniqueIdentifierText);

				if (oneEntryText == entryText)
				{
					// In order to make the click work with "Pega"-rendered web pages, it is essential to ensure all necessary
					// JavaScript events are triggered.

					var js = (IJavaScriptExecutor) this.webDriver;

					js.ExecuteScript ("arguments[0].focus();", this.remoteElement);
					Thread.Sleep (100);

					js.ExecuteScript (
						"arguments[0].dispatchEvent(new MouseEvent ('mousedown', {bubbles : true}));",
						this.remoteElement);
					Thread.Sleep (100);

					js.ExecuteScript (
						"arguments[0].dispatchEvent(new MouseEvent ('mouseup', {bubbles : true}));",
						this.remoteElement);
					Thread.Sleep (100);

					js.ExecuteScript (
						"arguments[0].dispatchEvent(new MouseEvent ('click', {bubbles : true}));",
						this.remoteElement);
					Thread.Sleep (100);

					var tagName = oneTag.TagName.ToLowerInvariant ();
					var tagType = oneTag.GetAttribute ("type")?.ToLowerInvariant ();

					if (tagName == "input" && tagType == "radio")
					{
						js.ExecuteScript (
							"arguments[0].checked = true; arguments [0].dispatchEvent(new Event('change', {bubbles : true}));",
							oneTag);
					}
					else if (tagName == "option" && tagType == null)
					{
						/*

						// TODO:
						// TODO: Fix the below commented JavaScript to work with Pega-rendered web pages.
						// TODO:

						js.ExecuteScript (
							"arguments[0].scrollIntoView({block: 'center'});" +
							"arguments[0].focus();" +
							"setTimeout(function() {" +
							"  arguments[0].dispatchEvent(new MouseEvent('mousedown', {bubbles : true}));;" +
							"  arguments[0].dispatchEvent(new MouseEvent('mouseup', {bubbles : true}));;" +
							"  arguments[0].click();" +
							"  setTimeout(function() {" +
							"    arguments[0].selectedIndex = " + index.ToString () + ";" +
							"    arguments[0].value = '" + entryText + "';" +
							"    arguments[0].dispatchEvent(new Event('change', {bubbles : true}));" +
							"    arguments[0].dispatchEvent(new Event('input', {bubbles : true}));" +
							"    arguments[0].dispatchEvent(new KeyboardEvent('keydown', {key: 'Enter', bubbles : true}));" +
							"    arguments[0].dispatchEvent(new KeyboardEvent('keyup', {key: 'Enter', bubbles : true}));" +
							"    arguments[0].dispatchEvent(new KeyboardEvent('keypress', {key: 'Enter', bubbles : true}));" +
							"    setTimeout(function() {" +
							"      arguments[0].blur();" +
							"      arguments[0].dispatchEvent (new Event ('focusout', {bubbles : true}));" +
							"    }, 100);" +
							"  }, 100);" +
							"}, 100);",
							base.remoteElement);

						// TODO:
						// TODO: Fix the above commented JavaScript to work with Pega-rendered web pages.
						// TODO:

						 */

						try
						{
							oneTag.Click ();
						}
						catch
						{
						}
					}
					else
					{
						throw new NotImplementedException (
							$"The tag type '{tagName}' with type '{tagType}' is not supported in the multi-valued control '{this.uniqueIdentifierText}'.");
					}

					break;
				}

				index++;
			}
		}
	}
}