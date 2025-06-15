using System.Collections.ObjectModel;

using OpenQA.Selenium;

using OOSelenium.Framework.Abstractions;
using OOSelenium.Framework.Entities;

namespace OOSelenium.Framework.WebUIControls
{
	public sealed class RadioButtons
		: MultiValuedControlBase
	{
		public RadioButtons (ReadOnlyCollection<IWebElement> radioTags, string uniqueIdentifierText, LocateByWhat byWhat, IWebDriver webDriver)
			: base (radioTags, uniqueIdentifierText, byWhat, webDriver)
		{
			if (radioTags == null || radioTags.Count == 0)
			{
				throw new ArgumentException ("The provided collection of radio buttons is empty.", nameof (radioTags));
			}

			var firstTagName = radioTags [0].TagName.ToLower ();

			if (firstTagName != "input" || radioTags [0].GetAttribute ("type").ToLower () != "radio")
			{
				throw new ArgumentException ("The provided element is not a <input type='radio'> tag.", nameof (radioTags));
			}
		}

		public IList<TextValuePair> RadioOptions
		{
			get  { return base.entries; }
		}

		public TextValuePair SelectedRadio
		{
			get { return base.GetSelectedEntries ().FirstOrDefault (); }
		}

		public void SelectRadio (string textOfRadioToBeSelected)
		{
			base.ClickAndSelectEntry (textOfRadioToBeSelected);
		}
	}
}